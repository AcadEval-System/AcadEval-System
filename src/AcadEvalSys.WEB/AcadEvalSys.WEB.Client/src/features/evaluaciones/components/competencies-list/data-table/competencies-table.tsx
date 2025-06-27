import { Search } from "lucide-react";
import { DataTable } from "@/shared/components/data-table/data-table";
import { columns } from "./columns";
import { Competency } from "../../../types";

interface CompetenciesTableProps {
  competencies: Competency[];
}

export function CompetenciesTable({ competencies }: CompetenciesTableProps) {
  return (
    <>
      {competencies.length > 0 ? (
        <DataTable columns={columns} data={competencies} />
      ) : (
        <div className="flex flex-col items-center justify-center py-8">
          <Search className="h-10 w-10 text-muted-foreground mb-2" />
          <h3 className="text-lg font-medium">
            No se encontraron competencias
          </h3>
          <p className="text-sm text-muted-foreground mt-1">
            Crea una nueva competencia
          </p>
        </div>
      )}
    </>
  );
}
