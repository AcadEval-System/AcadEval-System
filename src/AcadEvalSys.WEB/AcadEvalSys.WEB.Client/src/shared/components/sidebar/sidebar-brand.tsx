import { Link } from "wouter";
import { Layers } from "lucide-react";
import { SidebarHeader } from "@/shared/components/ui/sidebar";

export function SidebarBrand() {
  return (
    <SidebarHeader className="border-b border-border">
      <Link
        href="/dashboard"
        className="flex items-center gap-2 font-semibold px-4 py-3"
      >
        <Layers className="h-6 w-6 text-red-500 flex-shrink-0" />
        <span className="text-xl truncate">EVAC-ITEC</span>
      </Link>
    </SidebarHeader>
  );
}
