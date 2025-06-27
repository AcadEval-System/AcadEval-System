import { Input } from "@/shared/components/ui/input";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/shared/components/ui/select";
import { Search } from "lucide-react";

interface EvaluationFiltersProps {
  searchTerm: string;
  onSearchChange: (value: string) => void;
  statusFilter: string;
  onStatusFilterChange: (value: string) => void;
}

export function EvaluationFilters({
  searchTerm,
  onSearchChange,
  statusFilter,
  onStatusFilterChange,
}: EvaluationFiltersProps) {
  return (
    <div className="flex flex-col sm:flex-row gap-4 items-start sm:items-center w-full">
      <div className="relative w-full">
        <Search className="absolute left-2.5 top-2.5 h-4 w-4 text-muted-foreground font-medium" />
        <Input
          type="search"
          placeholder="Buscar evaluación..."
          className="pl-8 font-medium w-full"
          value={searchTerm}
          onChange={(e) => onSearchChange(e.target.value)}
        />
      </div>
      <Select value={statusFilter} onValueChange={onStatusFilterChange}>
        <SelectTrigger className="w-full sm:w-[180px] flex-shrink-0">
          <SelectValue placeholder="Filtrar por estado" />
        </SelectTrigger>
        <SelectContent>
          <SelectItem value="all">Todos los estados</SelectItem>
          <SelectItem value="active">En progreso</SelectItem>
          <SelectItem value="completed">Completadas</SelectItem>
          <SelectItem value="upcoming">Próximas</SelectItem>
        </SelectContent>
      </Select>
    </div>
  );
}
