import { useState } from "react";
import {
  CompetencyHeader,
  CompetencyFilters,
} from "../components/competencies-list";
import { useGetCompetencies } from "../hooks/competencies/queries/use-get-competencies";
import { CompetenciesGrid } from "../components/competencies-list/competencies-grid";

export default function CompetenciasPage() {
  const { data: competencies } = useGetCompetencies();
  const [searchTerm, setSearchTerm] = useState("");
  const [typeFilter, setTypeFilter] = useState("all");

  return (
    <div className="w-full space-y-6 max-w-full">
      <CompetencyHeader onNewCompetency={() => {}} />

      <CompetencyFilters
        searchTerm={searchTerm}
        onSearchChange={setSearchTerm}
        typeFilter={typeFilter}
        onTypeFilterChange={setTypeFilter}
      />

      <CompetenciesGrid competencies={competencies ?? []} />
    </div>
  );
}
