import { useState } from "react";
import {
  CompetencyHeader,
  CompetencyFilters,
} from "../components/competencies-list";
import { useGetCompetencies } from "../hooks/competencies/queries/use-get-competencies";
import { CardGrid } from "@/shared/components/card-grid";
import { CompetencyCard } from "../components/competencies-list/competency-card";
import { ContainerPage } from "@/shared/components/container-page";
import { SkeletonWrapper } from "@/shared/components/skeleton-wrapper";

export default function CompetenciasPage() {
  const { data: competencies, isLoading } = useGetCompetencies();
  const [searchTerm, setSearchTerm] = useState("");
  const [typeFilter, setTypeFilter] = useState("all");

  return (
    <ContainerPage>
      <CompetencyHeader onNewCompetency={() => {}} />

      <CompetencyFilters
        searchTerm={searchTerm}
        onSearchChange={setSearchTerm}
        typeFilter={typeFilter}
        onTypeFilterChange={setTypeFilter}
      />

      <SkeletonWrapper isLoading={isLoading} variant="grid">
        <CardGrid
          data={competencies ?? []}
          keyExtractor={(competency) => competency.id}
          children={(competency) => <CompetencyCard competency={competency} />}
        />
      </SkeletonWrapper>
    </ContainerPage>
  );
}
