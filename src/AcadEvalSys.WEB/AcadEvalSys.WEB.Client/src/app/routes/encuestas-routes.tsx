import { Switch, Route } from "wouter";
import {
  CrearEncuestaPage,
  EncuestasPage,
  PlantillasPage,
} from "@/features/encuestas";

export default function EncuestasRoutes() {
  return (
    <Switch>
      <Route path="/encuestas" component={EncuestasPage} />
      <Route path="/encuestas/crear" component={CrearEncuestaPage} />
      <Route path="/encuestas/plantillas" component={PlantillasPage} />
    </Switch>
  );
}
