import { api } from "@/shared/config/axios";
import { Evaluation } from "../types";

const EVALUATIONS_API_URL = "/evaluation-periods";

export const getEvaluations = async () => {
  const { data } = await api.get(EVALUATIONS_API_URL);
  return data;
};

export const getEvaluationById = async (id: string) => {
  const { data } = await api.get(`${EVALUATIONS_API_URL}/${id}`);
  return data;
};

export const createEvaluation = async (evaluation: Evaluation) => {
  const { data } = await api.post(EVALUATIONS_API_URL, evaluation);
  return data;
};
