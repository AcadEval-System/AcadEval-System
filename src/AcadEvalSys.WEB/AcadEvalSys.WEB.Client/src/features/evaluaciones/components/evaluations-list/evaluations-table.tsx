import { Search } from "lucide-react";
import { DataTable } from "@/shared/components/data-table/data-table";
import { columns } from "./data-table/columns";
import { Evaluation } from "../../types";

interface EvaluationsTableProps {
  evaluations: Evaluation[];
}

export function EvaluationsTable({ evaluations }: EvaluationsTableProps) {
  return (
    <>
      {evaluations.length > 0 ? (
        <DataTable columns={columns} data={evaluations} />
      ) : (
        <div className="flex flex-col items-center justify-center py-8">
          <Search className="h-10 w-10 text-muted-foreground mb-2" />
          <h3 className="text-lg font-medium">
            No se encontraron evaluaciones
          </h3>
          <p className="text-sm text-muted-foreground mt-1">
            Crea una nueva evaluación para comenzar
          </p>
        </div>
      )}
    </>
  );
}
