import { BadgeCheck, LogOut } from "lucide-react";
import { useCurrentUser } from "@/shared/auth/hooks/use-current-user";
import { AvatarDropdown } from "./avatar-dropdown";
import {
  DropdownMenuItem,
  DropdownMenuGroup,
  DropdownMenuSeparator,
} from "@/shared/components/ui/dropdown-menu";
import { useCallback, useState } from "react";
import { authService } from "@/shared/auth/services/auth-service";

export function UserDropdown() {
  const { user, initials } = useCurrentUser();
  const [isLoggingOut, setIsLoggingOut] = useState(false);

  const userInfo = {
    name: user?.name,
    email: user?.email,
    initials: initials,
    avatarUrl: undefined,
  };

  const handleLogout = useCallback(async () => {
    setIsLoggingOut(true);
    await authService.logout();
    setIsLoggingOut(false);
  }, []);

  return (
    <AvatarDropdown user={userInfo}>
      <DropdownMenuGroup>
        <DropdownMenuItem className="cursor-pointer">
          <BadgeCheck className="mr-2 h-4 w-4 flex-shrink-0" />
          <span className="truncate">Mi Cuenta</span>
        </DropdownMenuItem>
      </DropdownMenuGroup>
      <DropdownMenuSeparator />
      <DropdownMenuItem onClick={handleLogout} className="cursor-pointer">
        <LogOut className="mr-2 h-4 w-4 flex-shrink-0" />
        <span className="truncate">
          {isLoggingOut ? "Cerrando..." : "Cerrar Sesión"}
        </span>
      </DropdownMenuItem>
    </AvatarDropdown>
  );
}
