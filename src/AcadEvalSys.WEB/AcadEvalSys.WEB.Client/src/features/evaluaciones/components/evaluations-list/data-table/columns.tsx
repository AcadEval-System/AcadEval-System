"use client";

import { ColumnDef } from "@tanstack/react-table";
import { MoreHorizontal } from "lucide-react";
import { format } from "date-fns";

import { Button } from "@/shared/components/ui/button";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/shared/components/ui/dropdown-menu";
import { Badge } from "@/shared/components/ui/badge";
import { Evaluation } from "../../../types";
import { StatusBadge } from "@/shared/components/ui/status-badge";
import { ProgressIndicator } from "@/shared/components/ui/progress-indicator";
import { DataTableColumnHeader } from "@/shared/components/data-table/data-table-column-header";

export const columns: ColumnDef<Evaluation>[] = [
  {
    id: "title",
    accessorKey: "title",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Evaluación" />
    ),
    cell: ({ row }) => {
      const evaluation = row.original;
      return (
        <div>
          <div className="font-medium truncate">{evaluation.title}</div>
          <div className="text-sm text-muted-foreground truncate">
            Creada el {format(new Date(evaluation.createdAt), "dd/MM/yyyy")}
          </div>
        </div>
      );
    },
    size: 200,
  },
  {
    id: "status",
    accessorKey: "status",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Estado" />
    ),
    cell: ({ row }) => {
      const evaluation = row.original;
      return <StatusBadge status={evaluation.status} />;
    },
    size: 100,
  },
  {
    id: "progress",
    accessorKey: "progress",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Progreso" />
    ),
    cell: ({ row }) => {
      const evaluation = row.original;
      return <ProgressIndicator value={evaluation.progress} />;
    },
    size: 120,
  },
  {
    id: "period",
    accessorKey: "startDate",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Período" />
    ),
    cell: ({ row }) => {
      const evaluation = row.original;
      return (
        <div>
          <div className="font-medium truncate">
            {format(new Date(evaluation.startDate), "dd/MM/yyyy")}
          </div>
          <div className="text-sm text-muted-foreground truncate">
            hasta {format(new Date(evaluation.endDate), "dd/MM/yyyy")}
          </div>
        </div>
      );
    },
    size: 120,
  },
  {
    id: "careers",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Carreras" />
    ),
    cell: ({ row }) => {
      const evaluation = row.original;
      return (
        <div className="flex flex-wrap gap-1">
          {evaluation.careers.map((career) => (
            <Badge
              key={career.id}
              variant="outline"
              className="bg-slate-50 inline-block"
            >
              {career.name}
            </Badge>
          ))}
        </div>
      );
    },
    size: 130,
  },
  {
    id: "actions",
    header: ({ column }) => (
      <DataTableColumnHeader column={column} title="Acciones" />
    ),
    cell: ({ row }) => {
      const evaluation = row.original;
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
              <DropdownMenuLabel>Acciones</DropdownMenuLabel>
              <DropdownMenuItem
                onClick={() => navigator.clipboard.writeText(evaluation.id)}
              >
                Copiar ID
              </DropdownMenuItem>
              <DropdownMenuSeparator />
              <DropdownMenuItem>Ver detalles</DropdownMenuItem>
              <DropdownMenuItem>Editar evaluación</DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        </div>
      );
    },
    size: 80,
  },
];
