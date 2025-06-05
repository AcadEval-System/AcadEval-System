import { ElementType } from "react";

export interface NavItem {
  href: string;
  icon: ElementType;
  label: string;
}

export interface NavGroup {
  title: string;
  items: NavItem[];
  className?: string;
}
