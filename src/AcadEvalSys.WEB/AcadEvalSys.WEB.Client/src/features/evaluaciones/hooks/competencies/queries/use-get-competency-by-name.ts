import { useQuery, useQueryClient } from "@tanstack/react-query";
import { Competency } from "../../../types";
import { competenciesKeys } from "./use-get-competencies";
import { nameToSlug } from "@/shared/lib/utils";

export const useGetCompetencyByName = (nameSlug: string) => {
  const queryClient = useQueryClient();

  return useQuery({
    queryKey: competenciesKeys.detail(nameSlug),
    queryFn: async (): Promise<Competency | undefined> => {
      const cachedCompetencies = queryClient.getQueryData<Competency[]>(
        competenciesKeys.lists()
      );

      if (cachedCompetencies) {
        const foundCompetency = cachedCompetencies.find(
          (competency) => nameToSlug(competency.name) === nameSlug
        );

        if (foundCompetency) {
          return foundCompetency;
        }
      }
    },
    enabled: !!nameSlug, // Solo ejecutar si tenemos el nameSlug
    staleTime: 5 * 60 * 1000, // 5 minutos
  });
};
