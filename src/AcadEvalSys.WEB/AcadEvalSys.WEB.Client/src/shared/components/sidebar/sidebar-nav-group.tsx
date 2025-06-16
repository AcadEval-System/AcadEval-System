import {
  SidebarGroup,
  SidebarMenu,
  SidebarGroupLabel,
} from "@/shared/components/ui/sidebar";
import { NavItem } from "./types";
import { NavMenuItem } from "./nav-menu-item";

interface SidebarNavGroupProps {
  title: string;
  items: NavItem[];
  className?: string;
  currentPath: string;
}

export function SidebarNavGroup({
  title,
  items,
  className,
  currentPath,
}: SidebarNavGroupProps) {
  return (
    <SidebarGroup className={className}>
      <SidebarGroupLabel>{title}</SidebarGroupLabel>
      <SidebarMenu>
        {items.map((item) => (
          <NavMenuItem
            key={item.href}
            item={item}
            isActive={currentPath === item.href}
          />
        ))}
      </SidebarMenu>
    </SidebarGroup>
  );
}
