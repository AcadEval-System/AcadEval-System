import { LoginForm } from "../components/login-form";
import logoLoginMOL from "@/assets/logo_login_MOL.png";

export function LoginPage() {
  return (
    <div className="w-full max-w-md mx-auto">
      <div className="text-center mb-8 flex flex-col items-center">
        <img src={logoLoginMOL} alt="Logo" className="mb-4" />

        <div className="mb-2">
          <h2 className="text-4xl font-bold text-primary tracking-widest">
            SISTEMA EVAC
          </h2>
        </div>

        <p className="text-sm text-muted-foreground mt-2">
          Ingrese sus credenciales para acceder al sistema
        </p>
      </div>

      <LoginForm />
    </div>
  );
}
