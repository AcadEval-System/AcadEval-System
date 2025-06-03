import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import { ThemeProvider } from "./shared/components/theme-toggle/theme-provider";
import { QueryClientProvider } from "@tanstack/react-query";
import { queryClient } from "@/shared/config/queryClient";
import { Route } from "wouter";
import { DashboardPage } from "./features/dashboard";
import { Switch } from "wouter";
import {
  CompetenciasPage,
  EvaluacionesPage,
  NuevaEvaluacionPage,
} from "./features/evaluaciones";
import {
  CrearEncuestaPage,
  EncuestasPage,
  PlantillasPage,
} from "./features/encuestas";
import { PersonalPage, TecnicaturasPage } from "./features/administracion";
import { AppLayout } from "./shared/components/layout";

const rootElement = document.getElementById("root");

createRoot(rootElement).render(
  <StrictMode>
    <QueryClientProvider client={queryClient}>
      <ThemeProvider defaultTheme="system">
        <AppLayout>
          <Switch>
            {/* Rutas de Dashboard  */}
            <Route path="/" component={DashboardPage} />

            {/* Rutas de Encuestas  */}
            <Route path="/surveys" component={EncuestasPage} />
            <Route path="/surveys/templates" component={PlantillasPage} />
            <Route path="/surveys/new" component={CrearEncuestaPage} />

            {/* Rutas de Competencias  */}
            <Route
              path="/evaluations/competencies"
              component={CompetenciasPage}
            />
            <Route path="/evaluations" component={EvaluacionesPage} />
            <Route path="/evaluations/new" component={NuevaEvaluacionPage} />

            {/* Rutas de Administración  */}
            <Route
              path="/administration/tecnicaturas"
              component={TecnicaturasPage}
            />
            <Route path="/administration/personal" component={PersonalPage} />

            <Route path="/:rest*">404, not found!</Route>
          </Switch>
        </AppLayout>
      </ThemeProvider>
    </QueryClientProvider>
  </StrictMode>
);
