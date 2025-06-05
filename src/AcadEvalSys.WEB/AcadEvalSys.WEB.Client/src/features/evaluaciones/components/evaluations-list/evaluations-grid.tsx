import { Card } from "@/shared/components/ui/card";
import { PlusCircle, Search } from "lucide-react";
import { EvaluationCard } from "./evaluation-card";
import { Evaluation } from "../../types";

interface EvaluationsGridProps {
  evaluations: Evaluation[];
  onCreateClick: () => void;
  onCardClick?: (id: string) => void;
}

export function EvaluationsGrid({
  evaluations,
  onCardClick,
}: EvaluationsGridProps) {
  return (
    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
      {/* Tarjetas de evaluaciones */}
      {evaluations.length > 0 ? (
        evaluations.map((evaluation) => (
          <EvaluationCard
            key={evaluation.id}
            evaluation={evaluation}
            onClick={() => onCardClick && onCardClick(evaluation.id)}
          />
        ))
      ) : (
        <div className="col-span-1 md:col-span-2 flex flex-col items-center justify-center py-12">
          <Search className="h-12 w-12 text-muted-foreground mb-4" />
          <h3 className="text-lg font-medium">
            No se encontraron evaluaciones
          </h3>
          <p className="text-muted-foreground mt-2">
            Crea una nueva evaluación para comenzar
          </p>
        </div>
      )}
    </div>
  );
}
