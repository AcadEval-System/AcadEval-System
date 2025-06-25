import { Search } from "lucide-react";
import { Input } from "@/shared/components/ui/input";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/shared/components/ui/select";

interface CompetencyFiltersProps {
  searchTerm: string;
  onSearchChange: (value: string) => void;
  typeFilter: string;
  onTypeFilterChange: (value: string) => void;
}

export function CompetencyFilters({
  searchTerm,
  onSearchChange,
  typeFilter,
  onTypeFilterChange,
}: CompetencyFiltersProps) {
  return (
    <div className="flex flex-col md:flex-row gap-4 items-start md:items-center">
      <div className="relative w-full md:w-auto flex-1">
        <Search className="absolute left-2.5 top-2.5 h-4 w-4 text-muted-foreground" />
        <Input
          type="search"
          placeholder="Buscar competencia..."
          className="pl-8"
          value={searchTerm}
          onChange={(e) => onSearchChange(e.target.value)}
        />
      </div>

      <Select value={typeFilter} onValueChange={onTypeFilterChange}>
        <SelectTrigger className="w-full md:w-[200px]">
          <SelectValue placeholder="Todos los tipos" />
        </SelectTrigger>
        <SelectContent>
          <SelectItem value="all">Todos los tipos</SelectItem>
          <SelectItem value="blanda">Competencias blandas</SelectItem>
          <SelectItem value="técnica">Competencias técnicas</SelectItem>
        </SelectContent>
      </Select>
    </div>
  );
}
