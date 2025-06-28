export interface Career {
  id: string;
  name: string;
}

export interface Evaluation {
  id: string;
  title: string;
  description: string;
  periodFrom: string;
  periodTo: string;
  careerAssignments: CareerAssignment[];
}

export interface CareerAssignment {
  technicalCareerId: string;
  technicalCareerName: string;
  assignmentsByYear: AssignmentsByYear;
  totalAssignments: number;
  totalProfessors: number;
  totalCompetencies: number;
  activeYears: string[];
}

export type CareerYear = "First" | "Second" | "Third";

export type AssignmentsByYear = {
  [key in CareerYear]: Assignment[];
};

export interface Assignment {
  assignmentId: string;
  year: string;
  competencyId: string;
  competencyName: string;
  competencyDescription: string;
  competencyType: string;
  professorId: string;
  professorName: string;
  professorEmail: string;
}

export interface Competency {
  id: string;
  name: string;
  description: string;
  type: "Soft" | "Hard";
}
