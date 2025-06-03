import { Link } from "wouter";
import {
  SidebarMenuItem,
  SidebarMenuButton,
} from "@/shared/components/ui/sidebar";
import { NavItem } from "./types";

interface NavMenuItemProps {
  item: NavItem;
  isActive: boolean;
}

export function NavMenuItem({ item, isActive }: NavMenuItemProps) {
  const Icon = item.icon;

  return (
    <SidebarMenuItem>
      <SidebarMenuButton
        asChild
        isActive={isActive}
        className="w-full truncate"
      >
        <Link href={item.href} className="w-full">
          <Icon className="h-4 w-4 flex-shrink-0" />
          <span className="truncate">{item.label}</span>
        </Link>
      </SidebarMenuButton>
    </SidebarMenuItem>
  );
}
