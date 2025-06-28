import { api } from "@/shared/config/axios";
import { Competency } from "../types";
import { CompetencyFormData } from "../schemas/competency";

const COMPETENCIES_API_URL = "/competencies";

export const getCompetencies = async (): Promise<Competency[]> => {
  const { data } = await api.get<Competency[]>(COMPETENCIES_API_URL);
  return data;
};

export const getCompetencyById = async (
  id: string
): Promise<Competency | null> => {
  const { data } = await api.get<Competency>(`${COMPETENCIES_API_URL}/${id}`);
  return data;
};

export const createCompetency = async (
  competencyData: CompetencyFormData
): Promise<Competency> => {
  const { data } = await api.post<Competency>(
    COMPETENCIES_API_URL,
    competencyData
  );
  return data;
};

export const updateCompetency = async (
  id: string,
  competencyData: CompetencyFormData
): Promise<Competency> => {
  const { data } = await api.put<Competency>(
    `${COMPETENCIES_API_URL}/${id}`,
    competencyData
  );
  return data;
};

export const deleteCompetency = async (id: string): Promise<void> => {
  await api.delete(`${COMPETENCIES_API_URL}/${id}`);
};
