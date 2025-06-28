import { useQuery } from "@tanstack/react-query";
import { getCompetencies } from "../../../services/competency-services";

// Claves de consulta para mantener el cache organizado
export const competenciesKeys = {
  all: ["competencies"] as const,
  lists: () => [...competenciesKeys.all, "list"] as const,
  detail: (name: string) => [...competenciesKeys.all, "detail", name] as const,
};

export const useGetCompetencies = () => {
  return useQuery({
    queryKey: competenciesKeys.lists(),
    queryFn: getCompetencies,
  });
};
