import { create } from "zustand";

// Types
export type RouteInfo = {
  section: string;
  page: string;
  path: string;
  parent?: string;
};

export type RouteConfig = {
  [path: string]: RouteInfo;
};

export type BreadcrumbItem = {
  label: string;
  path: string | null;
};

// Mapeo de secciones principales (solo necesitamos esto para traducción)
const sectionLabels: Record<string, string> = {
  dashboard: "Panel Principal",
  surveys: "Encuestas",
  evaluations: "Evaluaciones",
  administration: "Administración",
  tecnicaturas: "Tecnicaturas",
  personal: "Personal",
};

// Mapeo de páginas específicas (solo para casos especiales)
const pageLabels: Record<string, string> = {
  main: "Panel Principal",
  creation: "Crear Encuesta",
  templates: "Plantillas",
  new: "Nueva Evaluación",
  tecnicaturas: "Tecnicaturas",
  personal: "Personal",
  settings: "Configuración",
};

// Mapeo de rutas especiales que no siguen la estructura jerárquica estándar
const specialRoutes: Record<string, string[]> = {
  "/administration/tecnicaturas": ["tecnicaturas"],
  "/administration/personal": ["personal"],
};

type RouteState = {
  routeConfig: RouteConfig;
  currentPath: string;
  setCurrentPath: (path: string) => void;
  getCurrentPageInfo: () => RouteInfo;
  getBreadcrumbItems: () => BreadcrumbItem[];
};

const routeConfig: RouteConfig = {
  "/": { section: "Panel Principal", page: "Panel Principal", path: "/" },
  "/surveys": { section: "Encuestas", page: "Listado", path: "/surveys" },
  "/surveys/new": {
    section: "Encuestas",
    page: "Nueva Encuesta",
    path: "/surveys/new",
    parent: "/surveys",
  },
  "/surveys/templates": {
    section: "Encuestas",
    page: "Plantillas",
    path: "/surveys/templates",
    parent: "/surveys",
  },

  // Evaluaciones section
  "/competencies": {
    section: "Evaluaciones",
    page: "Competencias",
    path: "/competencies",
    parent: "/evaluations",
  },
  "/evaluations": {
    section: "Evaluaciones",
    page: "Listado",
    path: "/evaluations",
  },
  "/evaluations/new": {
    section: "Evaluaciones",
    page: "Nueva Evaluación",
    path: "/evaluations/new",
    parent: "/evaluations",
  },

  // Administración section

  "/administration/tecnicaturas": {
    section: "Administración",
    page: "Tecnicaturas",
    path: "/tecnicaturas",
  },
  "/administration/personal": {
    section: "Administración",
    page: "Personal",
    path: "/personal",
  },
};

// Función para capitalizar y formatear un segmento de ruta
const formatPathSegment = (segment: string): string => {
  // Si existe en el mapeo, usar esa etiqueta
  if (pageLabels[segment]) {
    return pageLabels[segment];
  }

  // Si no, formatear el segmento (capitalizar primera letra, reemplazar guiones por espacios)
  return segment.replace(/-/g, " ").replace(/^\w/, (c) => c.toUpperCase());
};

export const useRouteStore = create<RouteState>((set, get) => ({
  routeConfig,
  currentPath: "/",

  setCurrentPath: (path: string) => set({ currentPath: path }),

  getCurrentPageInfo: () => {
    const { routeConfig, currentPath } = get();

    if (currentPath && routeConfig[currentPath]) {
      return routeConfig[currentPath];
    }

    const pathParts = currentPath.split("/");
    const baseSection = "/" + pathParts[1];

    const sectionRoute = Object.keys(routeConfig).find(
      (route) => route.startsWith(baseSection) && routeConfig[route].section
    );

    if (sectionRoute) {
      return {
        section: routeConfig[sectionRoute].section,
        page: pathParts[pathParts.length - 1] || "Listado",
        path: currentPath,
      };
    }

    return { section: "Dashboard", page: "Panel Principal", path: "/" };
  },

  getBreadcrumbItems: () => {
    const { currentPath } = get();
    const items: BreadcrumbItem[] = [];

    // Ignorar rutas vacías
    if (!currentPath || currentPath === "/") {
      return [{ label: "Panel Principal", path: null }];
    }

    // Verificar si es una ruta especial
    if (specialRoutes[currentPath]) {
      const section = specialRoutes[currentPath][0];
      return [
        {
          label: sectionLabels[section] || formatPathSegment(section),
          path: null,
        },
      ];
    }

    // Dividir la ruta en segmentos
    const segments = currentPath.split("/").filter(Boolean);

    // Si no hay segmentos, devolver dashboard
    if (segments.length === 0) {
      return [{ label: "Panel Principal", path: null }];
    }

    // Obtener la sección principal
    const mainSection = segments[0];
    const sectionLabel =
      sectionLabels[mainSection] || formatPathSegment(mainSection);

    // Si solo hay un segmento, es la página actual
    if (segments.length === 1) {
      items.push({ label: sectionLabel, path: null });
      return items;
    }

    // Añadir la sección principal como enlace
    items.push({
      label: sectionLabel,
      path: `/${mainSection}`,
    });

    // Si hay una subsección específica (competencies -> evaluations)
    if (mainSection === "competencies") {
      items.push({
        label: sectionLabels["evaluations"] || "Evaluaciones",
        path: `/evaluations`,
      });
    }

    // Añadir la página actual
    const currentPage = segments[segments.length - 1];

    // Manejar casos especiales basados en el contexto
    let pageLabel = "";
    if (currentPage === "new") {
      // Diferenciar entre "new" en diferentes contextos
      if (mainSection === "surveys") {
        pageLabel = "Nueva Encuesta";
      } else if (mainSection === "evaluations") {
        pageLabel = "Nueva Evaluación";
      } else {
        pageLabel = "Nuevo";
      }
    } else {
      pageLabel = pageLabels[currentPage] || formatPathSegment(currentPage);
    }

    items.push({
      label: pageLabel,
      path: null,
    });

    return items;
  },
}));
