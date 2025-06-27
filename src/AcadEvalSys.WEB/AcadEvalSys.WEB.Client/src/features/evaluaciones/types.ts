export interface Career {
  id: string;
  name: string;
}

export interface Evaluation {
  id: string;
  title: string;
  status: "active" | "completed" | "upcoming" | "cancelled" | "default";
  progress: number;
  startDate: Date;
  endDate: Date;
  competenciesCount: number;
  studentsCount: number;
  careers: Career[];
  createdAt: Date;
  description?: string;
  type?: string;
}

export interface Competency {
  id: string;
  name: string;
  description: string;
  type: "soft" | "technical";
}
