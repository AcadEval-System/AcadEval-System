import { useParams, Link } from "wouter";
import { ChevronLeft, Brain, Target, Edit, Trash2 } from "lucide-react";
import { useGetCompetencyByName } from "../hooks/competencies/queries/use-get-competency-by-name";
import { Button } from "@/shared/components/ui/button";
import { Badge } from "@/shared/components/ui/badge";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/shared/components/ui/card";
import { Skeleton } from "@/shared/components/ui/skeleton";
import { ContainerPage } from "@/shared/components/container-page";

export default function CompetencyDetailPage() {
  const { name } = useParams();
  const {
    data: competency,
    isLoading,
    error,
  } = useGetCompetencyByName(name || "");

  if (isLoading) {
    return (
      <ContainerPage>
        <div className="space-y-6">
          <Skeleton className="h-16 w-full" />
          <Skeleton className="h-32 w-full" />
          <Skeleton className="h-24 w-full" />
        </div>
      </ContainerPage>
    );
  }

  if (error || !competency) {
    return (
      <ContainerPage>
        <Link href="/evaluations/competencies">
          <Button variant="ghost" className="gap-2">
            <ChevronLeft className="w-4 h-4" />
            Volver a Competencias
          </Button>
        </Link>
        <Card className="border-destructive/50">
          <CardContent className="pt-6 text-center">
            <h3 className="text-lg font-semibold text-destructive">
              Competencia no encontrada
            </h3>
            <p className="text-sm text-muted-foreground">
              La competencia "{name}" no existe.
            </p>
          </CardContent>
        </Card>
      </ContainerPage>
    );
  }

  return (
    <ContainerPage>
      {/* Header */}
      <div className="flex items-center justify-between">
        <div className="flex items-center gap-4">
          <Link href="/evaluations/competencies">
            <Button variant="ghost" size="sm" className="gap-2">
              <ChevronLeft className="w-4 h-4" />
              Volver
            </Button>
          </Link>

          <div className="flex items-center gap-3">
            <div
              className={`w-10 h-10 rounded-lg flex items-center justify-center border border-border/40 ${
                competency.type === "Soft"
                  ? "bg-primary/10 text-primary"
                  : "bg-chart-4/10 text-chart-4"
              }`}
            >
              {competency.type === "Soft" ? (
                <Brain className="w-5 h-5" />
              ) : (
                <Target className="w-5 h-5" />
              )}
            </div>

            <div>
              <h1 className="text-2xl font-bold text-foreground">
                {competency.name}
              </h1>
              <Badge
                variant="secondary"
                className={`mt-1 ${
                  competency.type === "Soft"
                    ? "bg-primary/10 text-primary"
                    : "bg-chart-4/10 text-chart-4"
                }`}
              >
                {competency.type === "Soft"
                  ? "Competencias Blandas"
                  : "Competencias Duras"}
              </Badge>
            </div>
          </div>
        </div>

        <div className="flex items-center gap-2">
          <Button variant="outline" size="sm" className="gap-2">
            <Edit className="w-4 h-4" />
            Editar
          </Button>
          <Button
            variant="outline"
            size="sm"
            className="gap-2 text-destructive hover:text-destructive"
          >
            <Trash2 className="w-4 h-4" />
            Eliminar
          </Button>
        </div>
      </div>

      {/* Descripción */}
      <Card>
        <CardHeader>
          <CardTitle>Descripción</CardTitle>
          <CardDescription>
            Información detallada sobre esta competencia
          </CardDescription>
        </CardHeader>
        <CardContent>
          <p className="text-sm text-muted-foreground leading-relaxed">
            {competency.description}
          </p>
        </CardContent>
      </Card>

      {/* Evaluaciones relacionadas */}
      <Card>
        <CardHeader>
          <CardTitle>Evaluaciones Relacionadas</CardTitle>
          <CardDescription>
            Evaluaciones que incluyen esta competencia
          </CardDescription>
        </CardHeader>
        <CardContent>
          <p className="text-sm text-muted-foreground">
            Próximamente: Lista de evaluaciones que incluyen esta competencia
          </p>
        </CardContent>
      </Card>
    </ContainerPage>
  );
}
