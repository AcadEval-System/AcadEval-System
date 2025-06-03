import { ReactNode, useEffect } from "react";
import { usePathname } from "wouter/use-browser-location";
import { AppSidebar } from "./sidebar/app-sidebar";
import { SidebarInset, SidebarProvider, SidebarTrigger } from "./ui/sidebar";
import { Separator } from "./ui/separator";
import {
  Breadcrumb,
  BreadcrumbItem,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbPage,
  BreadcrumbSeparator,
} from "@/shared/components/ui/breadcrumb";
import { ThemeToggle } from "./theme-toggle/theme-toggle";
import {
  useRouteStore,
  BreadcrumbItem as BreadcrumbItemType,
} from "../stores/route-store";

interface AppLayoutProps {
  children: ReactNode;
}

export function AppLayout({ children }: AppLayoutProps) {
  const pathname = usePathname();
  const { setCurrentPath, getBreadcrumbItems } = useRouteStore();

  useEffect(() => {
    setCurrentPath(pathname);
  }, [pathname, setCurrentPath]);

  const breadcrumbItems = getBreadcrumbItems();

  return (
    <SidebarProvider defaultOpen>
      <AppSidebar />
      <SidebarInset>
        <header className="flex justify-between h-[4.30rem] shrink-0 items-center gap-2 border-b-[1.7px] transition-[wºidth,height] ease-linear">
          <div className="flex items-center gap-2 px-4">
            <SidebarTrigger className="-ml-1" />
            <Separator
              orientation="vertical"
              className="mr-2 data-[orientation=vertical]:h-4"
            />
            <Breadcrumb>
              <BreadcrumbList>
                {breadcrumbItems.flatMap(
                  (item: BreadcrumbItemType, index: number) => {
                    const isFirstItem = index === 0;
                    const isLastItem = index === breadcrumbItems.length - 1;

                    const elements = [
                      <BreadcrumbItem
                        key={`item-${index}`}
                        className={isFirstItem ? "hidden md:block" : ""}
                      >
                        {item.path ? (
                          <BreadcrumbLink href={item.path}>
                            {item.label}
                          </BreadcrumbLink>
                        ) : (
                          <BreadcrumbPage>{item.label}</BreadcrumbPage>
                        )}
                      </BreadcrumbItem>,
                    ];

                    if (!isLastItem) {
                      elements.push(
                        <BreadcrumbSeparator
                          key={`sep-${index}`}
                          className="hidden md:block"
                        />
                      );
                    }

                    return elements;
                  }
                )}
              </BreadcrumbList>
            </Breadcrumb>
          </div>
          <div className="flex items-center gap-2 mr-4">
            <ThemeToggle />
          </div>
        </header>
        <div className="flex flex-1 flex-col p-6">{children}</div>
      </SidebarInset>
    </SidebarProvider>
  );
}
