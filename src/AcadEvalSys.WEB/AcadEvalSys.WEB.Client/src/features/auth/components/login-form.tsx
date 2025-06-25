import { useForm, Controller } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import { Button } from "@/shared/components/ui/button";
import { Input } from "@/shared/components/ui/input";
import { Checkbox } from "@/shared/components/ui/checkbox";
import { useLoginMutation } from "@/shared/auth/hooks/use-login-mutation";
import { Loader2 } from "lucide-react";

const loginSchema = z.object({
  username: z.string().min(1, "El usuario es requerido"),
  password: z.string().min(1, "La contraseña es requerida"),
  rememberMe: z.boolean().optional(),
});

type LoginFormData = z.infer<typeof loginSchema>;

export const LoginForm = () => {
  const {
    register,
    handleSubmit,
    control,
    formState: { errors },
  } = useForm<LoginFormData>({
    resolver: zodResolver(loginSchema),
    defaultValues: {
      username: "",
      password: "",
      rememberMe: false,
    },
  });

  const {
    mutate: login,
    isPending: isLoading,
    error: loginError,
  } = useLoginMutation();

  const onSubmit = async (data: LoginFormData) => {
    try {
      await login({
        email: data.username,
        password: data.password,
      });
    } catch (error) {
      console.error("Error en submit:", error);
    }
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
      <div>
        <Input
          type="text"
          placeholder="alumno@itec-elmolino.edu.ar"
          className="w-full h-12 px-4 bg-input border border-border text-foreground placeholder:text-muted-foreground"
          disabled={isLoading}
          {...register("username")}
        />
        {errors.username && (
          <p className="text-destructive text-sm mt-1">
            {errors.username.message}
          </p>
        )}
      </div>

      <div>
        <Input
          type="password"
          placeholder="••••••••••••••"
          className="w-full h-12 px-4 bg-input border border-border text-foreground placeholder:text-muted-foreground"
          disabled={isLoading}
          {...register("password")}
        />
        {errors.password && (
          <p className="text-destructive text-sm mt-1">
            {errors.password.message}
          </p>
        )}
      </div>

      {/* Checkbox Mantener sesión */}
      <div className="flex items-center space-x-2 py-2">
        <Controller
          name="rememberMe"
          control={control}
          render={({ field }) => (
            <Checkbox
              id="rememberMe"
              checked={field.value}
              onCheckedChange={field.onChange}
              disabled={isLoading}
              className="border-border"
            />
          )}
        />
        <label
          htmlFor="rememberMe"
          className="text-sm text-muted-foreground cursor-pointer"
        >
          Mantener la sesión activa
        </label>
      </div>

      {/* Mostrar error de login si existe */}
      {loginError && (
        <div className="text-destructive text-sm text-center p-2 bg-destructive/10 rounded-md">
          {loginError.message || "Error al iniciar sesión"}
        </div>
      )}

      <Button
        type="submit"
        disabled={isLoading}
        className="w-full h-12 bg-primary hover:bg-primary/90 text-primary-foreground font-medium text-base"
      >
        {isLoading ? (
          <>
            <Loader2 className="mr-2 h-4 w-4 animate-spin" />
            Iniciando sesión...
          </>
        ) : (
          "Ingresar"
        )}
      </Button>

      <div className="text-center space-y-2 pt-4">
        <p>
          <a
            href="/auth/forgot-password"
            className="text-primary hover:text-primary/80 text-sm underline-offset-4 hover:underline"
          >
            ¿Olvidó su contraseña? - Recuperar contraseña
          </a>
        </p>
      </div>
    </form>
  );
};
