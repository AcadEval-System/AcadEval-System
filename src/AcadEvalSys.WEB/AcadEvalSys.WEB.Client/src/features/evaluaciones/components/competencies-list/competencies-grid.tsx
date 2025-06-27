import { Competency } from "../../types";
import { CompetencyCard } from "./competency-card";

interface CompetenciesGridProps {
  competencies: Competency[];
}

export function CompetenciesGrid({ competencies }: CompetenciesGridProps) {
  return (
    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
      {competencies.map((competency) => (
        <CompetencyCard key={competency.id} competency={competency} />
      ))}
    </div>
  );
}
