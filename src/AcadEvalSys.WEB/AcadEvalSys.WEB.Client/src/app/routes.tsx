import { Switch, Route } from "wouter";
import { DashboardPage } from "@/features/dashboard";
import EncuestasRoutes from "@/app/routes/encuestas-routes";
import CompetenciasRoutes from "@/app/routes/competencias-routes";
import AdministracionRoutes from "./routes/administracion-routes";

export default function Routes() {
  return (
    <Switch>
      <Route path="/" component={DashboardPage} />
      <EncuestasRoutes />
      <CompetenciasRoutes />
      <AdministracionRoutes />
      <Route>404: Not Found</Route>
    </Switch>
  );
}
