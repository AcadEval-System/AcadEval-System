import { Switch, Route } from "wouter";
import {
  NuevaEvaluacionPage as CrearEvaluacionPage,
  CompetenciasPage,
  EvaluacionesPage,
} from "@/features/evaluaciones";

export default function CompetenciasRoutes() {
  return (
    <Switch>
      <Route path="/competencias" component={CompetenciasPage} />
      <Route path="/competencias/evaluaciones" component={EvaluacionesPage} />
      <Route
        path="/competencias/evaluaciones/crear"
        component={CrearEvaluacionPage}
      />
    </Switch>
  );
}
