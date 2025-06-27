import { useUser } from "../stores/auth-store";

/**
 * Hook simple para obtener información del usuario actual
 */
export const useCurrentUser = () => {
  const user = useUser();

  const getUserInitials = (name: string) => {
    return name
      .split(" ")
      .map((part) => part.charAt(0).toUpperCase())
      .slice(0, 2)
      .join("");
  };

  return {
    user,
    initials: user?.name ? getUserInitials(user.name) : "",
  };
};
