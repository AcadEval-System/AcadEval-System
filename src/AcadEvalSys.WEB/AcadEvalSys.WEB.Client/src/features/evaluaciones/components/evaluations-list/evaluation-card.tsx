import { format } from "date-fns";
import {
  Card,
  CardContent,
  CardHeader,
  CardTitle,
} from "@/shared/components/ui/card";
import { Badge } from "@/shared/components/ui/badge";
import { StatusBadge } from "@/shared/components/ui/status-badge";
import { ProgressIndicator } from "@/shared/components/ui/progress-indicator";
import { Evaluation } from "../../types";

interface EvaluationCardProps {
  evaluation: Evaluation;
  onClick?: () => void;
}

export function EvaluationCard({ evaluation, onClick }: EvaluationCardProps) {
  return (
    <Card
      className="cursor-pointer hover:shadow-md transition-shadow"
      onClick={onClick}
    >
      <CardHeader className="pb-2">
        <div className="flex justify-between items-start mb-2">
          <StatusBadge status={evaluation.status} />
          <ProgressIndicator value={evaluation.progress} size="sm" />
        </div>
        <CardTitle className="text-lg">{evaluation.title}</CardTitle>
        <div className="text-sm text-muted-foreground mt-1">
          Creada el {format(new Date(evaluation.createdAt), "dd/MM/yyyy")}
        </div>
      </CardHeader>
      <CardContent>
        <div className="grid grid-cols-2 gap-y-2 text-sm">
          <div>
            <div className="font-medium">Período:</div>
            <div>
              {format(new Date(evaluation.startDate), "dd/MM/yyyy")} -{" "}
              {format(new Date(evaluation.endDate), "dd/MM/yyyy")}
            </div>
          </div>
          <div>
            <div className="font-medium">Competencias:</div>
            <div>{evaluation.competenciesCount}</div>
          </div>
          <div>
            <div className="font-medium">Estudiantes:</div>
            <div>{evaluation.studentsCount}</div>
          </div>
          <div>
            <div className="font-medium text-sm mb-1">Carreras:</div>
          </div>
          <div className="flex flex-wrap gap-1">
            {evaluation.careers.map((career) => (
              <Badge key={career.id} variant="outline" className="text-xs">
                {career.name}
              </Badge>
            ))}
          </div>
        </div>
      </CardContent>
    </Card>
  );
}
