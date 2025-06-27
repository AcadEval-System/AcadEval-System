import { useQuery } from "@tanstack/react-query";
import { getCompetencies } from "../../../services/competency-services";

// Claves de consulta para mantener el cache organizado
export const competenciesKeys = {
  all: ["competencies"] as const,
  lists: () => [...competenciesKeys.all, "list"] as const,
  list: (filters?: string) =>
    [...competenciesKeys.lists(), { filters }] as const,
  details: () => [...competenciesKeys.all, "detail"] as const,
  detail: (id: string) => [...competenciesKeys.details(), id] as const,
};

export const useGetCompetencies = () => {
  return useQuery({
    queryKey: competenciesKeys.lists(),
    queryFn: getCompetencies,
  });
};
