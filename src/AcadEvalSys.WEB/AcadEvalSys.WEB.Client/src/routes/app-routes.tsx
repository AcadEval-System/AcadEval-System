import { Route, Switch } from "wouter";
import { AppLayout } from "../shared/components/layout";
import { DashboardPage } from "../features/dashboard";
import {
  CompetenciasPage,
  EvaluacionesPage,
  NuevaEvaluacionPage,
  CompetencyDetailPage,
} from "../features/evaluaciones";
import {
  CrearEncuestaPage,
  EncuestasPage,
  PlantillasPage,
} from "../features/encuestas";
import { PersonalPage, TecnicaturasPage } from "../features/administracion";

export function AppRoutes() {
  return (
    <AppLayout>
      <Switch>
        <Route path="/" component={DashboardPage} />
        <Route path="/surveys" component={EncuestasPage} />
        <Route path="/surveys/templates" component={PlantillasPage} />
        <Route path="/surveys/new" component={CrearEncuestaPage} />
        <Route
          path="/evaluations/competencies/:name"
          component={CompetencyDetailPage}
        />
        <Route path="/evaluations/competencies" component={CompetenciasPage} />
        <Route path="/evaluations" component={EvaluacionesPage} />
        <Route path="/evaluations/new" component={NuevaEvaluacionPage} />
        <Route
          path="/administration/tecnicaturas"
          component={TecnicaturasPage}
        />
        <Route path="/administration/personal" component={PersonalPage} />
        <Route path="/:rest*">
          <div className="flex items-center justify-center min-h-[400px]">
            <div className="text-center">
              <h1 className="text-4xl font-bold mb-4">404</h1>
              <p className="text-gray-600">Página no encontrada</p>
            </div>
          </div>
        </Route>
      </Switch>
    </AppLayout>
  );
}
