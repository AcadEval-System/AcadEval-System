import { Switch, Route } from "wouter";
import { TecnicaturasPage, PersonalPage } from "@/features/administracion";

export default function AdministracionRoutes() {
  return (
    <Switch>
      <Route path="/administracion/tecnicaturas" component={TecnicaturasPage} />
      <Route path="/administracion/personal" component={PersonalPage} />
    </Switch>
  );
}
