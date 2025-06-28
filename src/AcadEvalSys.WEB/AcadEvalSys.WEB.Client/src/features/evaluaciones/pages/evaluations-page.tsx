import { useState } from "react";
import { EvaluationFilters } from "../components/evaluations-list/data-table/evaluations-filters";
import { EvaluationHeader } from "../components/evaluations-list/evaluation-header";
import { ContainerPage } from "@/shared/components/container-page";
import { useGetEvaluations } from "../hooks/evaluations/queries/use-get-evaluations";
import { DataTable } from "@/shared/components/data-table/data-table";
import { columns } from "../components/evaluations-list/data-table/columns";
import { SkeletonWrapper } from "@/shared/components/skeleton-wrapper";
import { Link } from "wouter";
import { navigate } from "wouter/use-browser-location";

export default function EvaluacionesPage() {
  const [searchTerm, setSearchTerm] = useState("");
  const [statusFilter, setStatusFilter] = useState("all");
  const [careerFilter, setCareerFilter] = useState("all");
  const { data: evaluations, isLoading, error } = useGetEvaluations();
  console.log(error);
  return (
    <ContainerPage>
      <EvaluationHeader onNewEvaluation={() => {}} onViewResults={() => {}} />
      <EvaluationFilters
        searchTerm={searchTerm}
        onSearchChange={setSearchTerm}
        statusFilter={statusFilter}
        onStatusFilterChange={setStatusFilter}
      />
      <SkeletonWrapper isLoading={isLoading}>
        <DataTable
          columns={columns}
          data={evaluations ?? []}
          onRowClick={(id) => {
            navigate(`/evaluaciones/${id}`);
          }}
        />
      </SkeletonWrapper>
    </ContainerPage>
  );
}
