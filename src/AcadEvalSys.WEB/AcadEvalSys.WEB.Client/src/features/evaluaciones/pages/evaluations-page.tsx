// src/AcadEvalSys.WEB/AcadEvalSys.WEB.Client/src/features/evaluaciones/pages/evaluaciones-page.tsx
import { useState } from "react";

import { Evaluation, Career } from "../types";
import { EvaluationFilters } from "../components/evaluations-list/data-table/evaluations-filters";
import { EvaluationsTable } from "../components/evaluations-list/data-table/evaluations-table";
import { EvaluationsGrid } from "../components/evaluations-list/evaluations-grid";
import { EvaluationHeader } from "../components/evaluations-list/evaluation-header";

// Datos de ejemplo para el mock
const MOCK_CAREERS: Career[] = [
  { id: "1", name: "Desarrollo de Software" },
  { id: "2", name: "Gestión Industrial" },
  { id: "3", name: "Mantenimiento Industrial" },
];

const MOCK_EVALUATIONS: Evaluation[] = [
  {
    id: "1",
    title: "Evaluación de Competencias 2023-1",
    status: "active",
    progress: 65,
    startDate: new Date("2023-02-28"),
    endDate: new Date("2023-03-14"),
    competenciesCount: 6,
    studentsCount: 28,
    careers: [{ id: "1", name: "Desarrollo de Software" }],
    createdAt: new Date("2023-02-14"),
  },
  {
    id: "2",
    title: "Evaluación de Competencias Técnicas 2023-1",
    status: "active",
    progress: 32,
    startDate: new Date("2023-03-09"),
    endDate: new Date("2023-04-23"),
    competenciesCount: 6,
    studentsCount: 42,
    careers: [{ id: "1", name: "Desarrollo de Software" }],

    createdAt: new Date("2023-02-27"),
  },
  {
    id: "3",
    title: "Evaluación de Competencias Blandas 2022-2",
    status: "completed",
    progress: 100,
    startDate: new Date("2022-11-04"),
    endDate: new Date("2022-11-19"),
    competenciesCount: 6,
    studentsCount: 36,
    careers: [{ id: "2", name: "Gestión Industrial" }],
    createdAt: new Date("2022-10-19"),
  },
  {
    id: "4",
    title: "Evaluación de Competencias por Niveles 2023-1",
    status: "upcoming",
    progress: 0,
    startDate: new Date("2023-04-04"),
    endDate: new Date("2023-04-19"),
    competenciesCount: 6,
    studentsCount: 45,
    careers: [{ id: "3", name: "Mantenimiento Industrial" }],
    createdAt: new Date("2023-03-14"),
  },
];

export default function EvaluacionesPage() {
  const [searchTerm, setSearchTerm] = useState("");
  const [statusFilter, setStatusFilter] = useState("all");
  const [careerFilter, setCareerFilter] = useState("all");
  const [viewMode, setViewMode] = useState<"table" | "grid">("table");

  // Filtrar evaluaciones según los criterios
  const filteredEvaluations = MOCK_EVALUATIONS.filter((evaluation) => {
    // Filtro por término de búsqueda
    const matchesSearch =
      searchTerm === "" ||
      evaluation.title.toLowerCase().includes(searchTerm.toLowerCase());

    // Filtro por estado
    const matchesStatus =
      statusFilter === "all" || evaluation.status === statusFilter;

    // Filtro por carrera
    const matchesCareer =
      careerFilter === "all" ||
      evaluation.careers.some((career) => career.id === careerFilter);

    return matchesSearch && matchesStatus && matchesCareer;
  });

  const handleCreateClick = () => {
    console.log("Crear nueva evaluación");
    // Aquí irías a la página de creación de evaluación
  };

  const handleViewResults = () => {
    console.log("Ver todos los resultados");
    // Aquí irías a la página de resultados de evaluaciones
  };

  const handleViewDetails = (id: string) => {
    console.log("Ver detalles de evaluación:", id);
    // Aquí irías a la página de detalles de evaluación
  };

  return (
    <div className="w-full space-y-6 max-w-full">
      <EvaluationHeader
        onNewEvaluation={handleCreateClick}
        onViewResults={handleViewResults}
      />

      <EvaluationFilters
        searchTerm={searchTerm}
        onSearchChange={setSearchTerm}
        statusFilter={statusFilter}
        onStatusFilterChange={setStatusFilter}
      />

      {viewMode === "table" ? (
        <EvaluationsTable evaluations={filteredEvaluations} />
      ) : (
        <EvaluationsGrid
          evaluations={filteredEvaluations}
          onCreateClick={handleCreateClick}
          onCardClick={handleViewDetails}
        />
      )}
    </div>
  );
}
