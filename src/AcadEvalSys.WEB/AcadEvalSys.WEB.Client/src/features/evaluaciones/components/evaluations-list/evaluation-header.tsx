import { FileBarChart } from "lucide-react";

import { Button } from "@/shared/components/ui/button";
import { PlusCircle } from "lucide-react";

interface EvaluationHeaderProps {
  onNewEvaluation: () => void;
  onViewResults: () => void;
}

export function EvaluationHeader({ onNewEvaluation }: EvaluationHeaderProps) {
  return (
    <div className="flex justify-between items-center">
      <div>
        <h2 className="text-3xl font-bold tracking-tight">
          Gestión de Evaluaciones
        </h2>
        <p className="text-muted-foreground">
          Administra las evaluaciones por competencias
        </p>
      </div>
      <div className="flex gap-3">
        <Button onClick={onNewEvaluation}>
          <PlusCircle className="mr-2 h-4 w-4" /> Nueva Evaluación
        </Button>
      </div>
    </div>
  );
}
