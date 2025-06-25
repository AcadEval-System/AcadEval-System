import { useState } from "react";
import { Competency } from "../types";
import {
  CompetencyHeader,
  CompetencyFilters,
  CompetenciesTable,
} from "../components/competencies-list";

const MOCK_COMPETENCIES: Competency[] = [
  {
    id: "1",
    name: "Competencia 1",
    description: "Descripción de la competencia 1",
    type: "soft",
  },
];

export default function CompetenciasPage() {
  const [competencies, setCompetencies] =
    useState<Competency[]>(MOCK_COMPETENCIES);
  const [searchTerm, setSearchTerm] = useState("");
  const [typeFilter, setTypeFilter] = useState("all");

  const filteredCompetencies = competencies
    .filter(
      (c) =>
        searchTerm === "" ||
        c.name.toLowerCase().includes(searchTerm.toLowerCase())
    )
    .filter((c) => typeFilter === "all" || c.type === typeFilter);

  const handleCreateClick = () => {
    console.log("Creando nueva competencia");
  };

  const handleEdit = (competency: Competency) => {
    console.log("Editando competencia:", competency.id);
  };

  const handleDelete = (id: string) => {
    console.log("Eliminando competencia:", id);
    // Aquí iría la lógica de eliminación
  };

  const handleViewDetails = (id: string) => {
    console.log("Ver detalles de competencia:", id);
    // Aquí iría la navegación a detalles
  };

  const handleDuplicate = (id: string) => {
    console.log("Duplicando competencia:", id);
    // Aquí iría la lógica de duplicación
  };

  const handleFormSubmit = (data: Competency) => {
    console.log("Datos del formulario:", data);

    // Mock: agregar nueva competencia
    const newCompetency: Competency = {
      id: `competency-${Date.now()}`,
      name: data.name,
      description: data.description,
      type: data.type,
    };

    setCompetencies((prev) => [...prev, newCompetency]);
  };

  return (
    <div className="w-full space-y-6 max-w-full">
      <CompetencyHeader onNewCompetency={handleCreateClick} />

      <CompetencyFilters
        searchTerm={searchTerm}
        onSearchChange={setSearchTerm}
        typeFilter={typeFilter}
        onTypeFilterChange={setTypeFilter}
      />

      <CompetenciesTable competencies={filteredCompetencies} />
    </div>
  );
}
