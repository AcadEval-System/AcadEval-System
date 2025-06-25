"use client";

import { ColumnDef } from "@tanstack/react-table";
import {
  MoreHorizontal,
  BookOpen,
  Briefcase,
  Edit,
  Eye,
  Copy,
  Trash2,
} from "lucide-react";
import { format } from "date-fns";

import { Button } from "@/shared/components/ui/button";
import { Badge } from "@/shared/components/ui/badge";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/shared/components/ui/dropdown-menu";
import { DataTableColumnHeader } from "@/shared/components/data-table/data-table-column-header";
import { Competency } from "../../../types";

export const columns: ColumnDef<Competency>[] = [
  {
    id: "competency",
    accessorKey: "name",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Competencia" />
    ),
    cell: ({ row }) => {
      const competency = row.original;
      return (
        <div>
          <div className="font-medium text-sm leading-tight">
            {competency.name}
          </div>
        </div>
      );
    },
    size: 300,
  },
  {
    id: "type",
    accessorKey: "type",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Tipo" />
    ),
    cell: ({ row }) => {
      const competency = row.original;
      return (
        <Badge
          variant="secondary"
          className={
            competency.type === "soft"
              ? "bg-blue-100 text-blue-800 hover:bg-blue-200"
              : "bg-amber-100 text-amber-800 hover:bg-amber-200"
          }
        >
          {competency.type === "soft" ? (
            <BookOpen className="mr-1 h-3 w-3" />
          ) : (
            <Briefcase className="mr-1 h-3 w-3" />
          )}
          {competency.type === "soft" ? "Blanda" : "Técnica"}
        </Badge>
      );
    },
    size: 120,
  },
  {
    id: "actions",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Acciones" />
    ),
    cell: ({ row }) => {
      const competency = row.original;
      return (
        <div className="text-right">
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="ghost" className="h-8 w-8 p-0">
                <span className="sr-only">Abrir menú</span>
                <MoreHorizontal className="h-4 w-4" />
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end">
              <DropdownMenuItem>
                <Eye className="mr-2 h-4 w-4" />
                <span>Ver detalles</span>
              </DropdownMenuItem>
              <DropdownMenuItem>
                <Edit className="mr-2 h-4 w-4" />
                <span>Editar</span>
              </DropdownMenuItem>
              <DropdownMenuItem>
                <Copy className="mr-2 h-4 w-4" />
                <span>Duplicar</span>
              </DropdownMenuItem>
              <DropdownMenuSeparator />
              <DropdownMenuItem className="text-red-600">
                <Trash2 className="mr-2 h-4 w-4" />
                <span>Eliminar</span>
              </DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        </div>
      );
    },
    size: 80,
  },
];
