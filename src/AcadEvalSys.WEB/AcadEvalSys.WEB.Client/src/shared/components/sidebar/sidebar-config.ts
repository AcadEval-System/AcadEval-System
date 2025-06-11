import {
  FileText,
  Users,
  Settings,
  PlusCircle,
  Layers,
  LayoutDashboard,
  BookOpenCheck,
  ClipboardCheck,
  GraduationCap,
} from "lucide-react";
import { NavGroup } from "./types";

export const sidebarConfig: Record<string, NavGroup> = {
  main: {
    title: "Inicio",
    items: [
      {
        href: "/",
        icon: LayoutDashboard,
        label: "Panel Principal",
      },
    ],
  },
  surveys: {
    title: "Encuestas Académicas",
    items: [
      {
        href: "/surveys",
        icon: FileText,
        label: "Encuestas",
      },
      {
        href: "/surveys/templates",
        icon: Layers,
        label: "Plantillas",
      },
      {
        href: "/surveys/new",
        icon: PlusCircle,
        label: "Crear Encuesta",
      },
    ],
  },
  evaluations: {
    title: "Evaluaciones por Competencias",
    items: [
      {
        href: "/evaluations",
        icon: ClipboardCheck,
        label: "Evaluaciones",
      },
      {
        href: "/evaluations/competencies",
        icon: BookOpenCheck,
        label: "Competencias",
      },
      {
        href: "/evaluations/new",
        icon: PlusCircle,
        label: "Nueva Evaluación",
      },
    ],
  },
  administration: {
    title: "Administración",
    items: [
      {
        href: "/administration/tecnicaturas",
        icon: GraduationCap,
        label: "Tecnicaturas",
      },
      {
        href: "/administration/personal",
        icon: Users,
        label: "Personal",
      },
    ],
  },
};
