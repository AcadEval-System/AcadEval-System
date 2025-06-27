import {
  Card,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/shared/components/ui/card";
import { Competency } from "../../types";
import { Badge } from "@/shared/components/ui/badge";
import { Link } from "wouter";
import { ChevronRight, Target, Brain } from "lucide-react";

interface CompetencyCardProps {
  competency: Competency;
}

export function CompetencyCard({ competency }: CompetencyCardProps) {
  return (
    <Link href={`/evaluations/competencies/${competency.id}`}>
      <Card className="group w-full h-full p-0 border border-border/40 shadow-sm hover:shadow-lg hover:border-border/80 transition-all duration-300 cursor-pointer bg-gradient-to-br from-white to-gray-50/30 dark:from-gray-900 dark:to-gray-800/30 hover:scale-[1.02] active:scale-[0.98]">
        <CardHeader className="p-6 space-y-4">
          <div className="flex justify-between items-start gap-3">
            <div className="flex items-center gap-3 flex-1 min-w-0">
              <div className="flex-shrink-0 w-10 h-10 rounded-lg bg-gradient-to-br from-blue-500/10 to-purple-500/10 dark:from-blue-400/20 dark:to-purple-400/20 flex items-center justify-center border border-blue-200/20 dark:border-blue-400/20">
                {competency.type === "soft" ? (
                  <Brain className="w-5 h-5 text-blue-600 dark:text-blue-400" />
                ) : (
                  <Target className="w-5 h-5 text-purple-600 dark:text-purple-400" />
                )}
              </div>

              <div className="flex-1 min-w-0">
                <CardTitle className="text-lg font-semibold text-gray-900 dark:text-gray-100 group-hover:text-blue-700 dark:group-hover:text-blue-300 transition-colors duration-200 truncate">
                  {competency.name}
                </CardTitle>
              </div>
            </div>

            <div className="flex items-center gap-2 flex-shrink-0">
              <Badge
                variant="outline"
                className={`font-medium text-xs px-3 py-1 rounded-full border-0 ${
                  competency.type === "soft"
                    ? "bg-gradient-to-r from-blue-50 to-indigo-50 text-blue-700 dark:from-blue-900/30 dark:to-indigo-900/30 dark:text-blue-300"
                    : "bg-gradient-to-r from-purple-50 to-pink-50 text-purple-700 dark:from-purple-900/30 dark:to-pink-900/30 dark:text-purple-300"
                }`}
              >
                {competency.type === "soft" ? "Blandas" : "Duras"}
              </Badge>

              <ChevronRight className="w-4 h-4 text-gray-400 group-hover:text-gray-600 dark:group-hover:text-gray-300 group-hover:translate-x-1 transition-all duration-200" />
            </div>
          </div>

          <CardDescription className="text-sm text-gray-600 dark:text-gray-300 leading-relaxed line-clamp-2 group-hover:text-gray-700 dark:group-hover:text-gray-200 transition-colors duration-200">
            {competency.description}
          </CardDescription>

          {/* Indicador visual de interactividad */}
          <div className="w-full h-1 bg-gradient-to-r from-blue-200/40 to-purple-200/40 dark:from-blue-600/20 dark:to-purple-600/20 rounded-full opacity-0 group-hover:opacity-100 transition-opacity duration-300" />
        </CardHeader>
      </Card>
    </Link>
  );
}
