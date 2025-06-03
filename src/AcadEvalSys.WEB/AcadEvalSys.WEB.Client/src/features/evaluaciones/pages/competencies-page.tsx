import { useState } from "react";
// import { useRouter } from "wouter"; // Navegación deshabilitada para el mock
import {
  PlusCircle,
  Search,
  MoreHorizontal,
  Edit,
  Trash2,
  Copy,
  BookOpen,
  Briefcase,
  Eye,
  TableIcon,
  LayoutGrid,
  GraduationCap,
  Book,
  School,
  Save,
} from "lucide-react";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
  // CardFooter, // No usado directamente
} from "@/shared/components/ui/card";
import { Button } from "@/shared/components/ui/button";
import { Input } from "@/shared/components/ui/input";
import { Badge } from "@/shared/components/ui/badge";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/shared/components/ui/select";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/shared/components/ui/table";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  // DialogTrigger, // El trigger es el botón Nueva Competencia
} from "@/shared/components/ui/dialog";
import {
  Form,
  FormControl,
  FormDescription,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/shared/components/ui/form";
import { Textarea } from "@/shared/components/ui/textarea";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/shared/components/ui/dropdown-menu";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import {
  Tooltip,
  TooltipContent,
  TooltipProvider,
  TooltipTrigger,
} from "@/shared/components/ui/tooltip";
import { Checkbox } from "@/shared/components/ui/checkbox";
import { ScrollArea } from "@/shared/components/ui/scroll-area";

const TECNICATURAS_MOCK = [
  { id: "tec-1", name: "Tecnicatura en Desarrollo de Software" },
  { id: "tec-2", name: "Tecnicatura en Gestión Industrial" },
  { id: "tec-3", name: "Tecnicatura en Mantenimiento Industrial" },
  { id: "tec-4", name: "Tecnicatura en Logística" },
];

const YEARS_MOCK = [1, 2, 3];

const competencyFormSchema = z.object({
  name: z.string().min(3, "El nombre debe tener al menos 3 caracteres."),
  description: z
    .string()
    .min(10, "La descripción debe tener al menos 10 caracteres."),
  type: z.enum(["blanda", "técnica"], {
    required_error: "Debes seleccionar un tipo.",
  }),
  tecnicaturaYears: z.record(z.array(z.number())).refine(
    (data) => {
      return Object.values(data).some((years) => years.length > 0);
    },
    {
      message: "Debes seleccionar al menos un año para una tecnicatura.",
      path: [], // Puedes ajustar la ruta del error si es necesario
    }
  ),
});

export default function CompetenciasPage() {
  const [searchTerm, setSearchTerm] = useState("");
  const [typeFilter, setTypeFilter] = useState("all");
  const [tecnicaturaFilter, setTecnicaturaFilter] = useState("all");
  const [yearFilter, setYearFilter] = useState("all");
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [competencies, setCompetencies] = useState<any[]>([]); // Sin competencias iniciales
  const [editingCompetency, setEditingCompetency] = useState<any>(null);
  const [viewMode, setViewMode] = useState<"table" | "cards">("table");

  const form = useForm<z.infer<typeof competencyFormSchema>>({
    resolver: zodResolver(competencyFormSchema),
    defaultValues: {
      name: "",
      description: "",
      type: undefined,
      tecnicaturaYears: TECNICATURAS_MOCK.reduce(
        (acc, tec) => ({ ...acc, [tec.id]: [] }),
        {}
      ),
    },
  });

  const handleOpenForm = (competency: any = null) => {
    if (competency) {
      setEditingCompetency(competency);
      form.reset({
        name: competency.name,
        description: competency.description,
        type: competency.type,
      });
    } else {
      setEditingCompetency(null);
      form.reset({
        name: "",
        description: "",
        type: undefined,
      });
    }
    setIsDialogOpen(true);
  };

  const onSubmit = (values: z.infer<typeof competencyFormSchema>) => {
    console.log("Formulario enviado (mock):", values);
    if (editingCompetency) {
      console.log("Editando competencia (mock):", editingCompetency.id);
      // Lógica de edición mockeada
    } else {
      console.log("Creando nueva competencia (mock)");
      // Lógica de creación mockeada
    }
    setIsDialogOpen(false);
    setEditingCompetency(null);
  };

  const handleDelete = (id: string) => {
    console.log("Eliminando competencia (mock):", id);
  };

  const filteredCompetencies = competencies
    .filter(
      (c) =>
        searchTerm === "" ||
        c.name.toLowerCase().includes(searchTerm.toLowerCase())
    )
    .filter((c) => typeFilter === "all" || c.type === typeFilter)
    .filter(
      (c) =>
        tecnicaturaFilter === "all" ||
        c.tecnicaturas.includes(tecnicaturaFilter)
    )
    .filter(
      (c) => yearFilter === "all" || c.years.includes(parseInt(yearFilter))
    );

  const getTecnicaturaNames = (tecnicaturaIds: string[] = []) => {
    return tecnicaturaIds
      .map((id) => TECNICATURAS_MOCK.find((t) => t.id === id)?.name)
      .filter(Boolean) as string[];
  };

  return (
    <div className="space-y-6">
      <div className="flex justify-between items-center">
        <div>
          <h2 className="text-3xl font-bold tracking-tight">
            Gestión de Competencias (Mock)
          </h2>
          <p className="text-muted-foreground">
            Administra las competencias para evaluaciones (versión mock)
          </p>
        </div>
        <div className="flex items-center gap-2">
          <div className="flex border rounded-md overflow-hidden">
            <TooltipProvider>
              <Tooltip>
                <TooltipTrigger asChild>
                  <Button
                    variant={viewMode === "table" ? "default" : "ghost"}
                    size="sm"
                    className="rounded-none px-3"
                    onClick={() => setViewMode("table")}
                  >
                    <TableIcon className="h-4 w-4" />
                    <span className="sr-only">Vista de tabla</span>
                  </Button>
                </TooltipTrigger>
                <TooltipContent>
                  <p>Vista de tabla</p>
                </TooltipContent>
              </Tooltip>
            </TooltipProvider>
            <TooltipProvider>
              <Tooltip>
                <TooltipTrigger asChild>
                  <Button
                    variant={viewMode === "cards" ? "default" : "ghost"}
                    size="sm"
                    className="rounded-none px-3"
                    onClick={() => setViewMode("cards")}
                  >
                    <LayoutGrid className="h-4 w-4" />
                    <span className="sr-only">Vista de tarjetas</span>
                  </Button>
                </TooltipTrigger>
                <TooltipContent>
                  <p>Vista de tarjetas</p>
                </TooltipContent>
              </Tooltip>
            </TooltipProvider>
          </div>
        </div>
      </div>

      <div className="flex flex-col gap-4">
        <div className="flex flex-col md:flex-row gap-4 items-start md:items-center">
          <div className="relative w-full md:w-auto flex-1">
            <Search className="absolute left-2.5 top-2.5 h-4 w-4 text-muted-foreground" />
            <Input
              type="search"
              placeholder="Buscar competencias..."
              className="pl-8"
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
            />
          </div>

          <Select
            value={typeFilter}
            onValueChange={(value) => setTypeFilter(value)}
          >
            <SelectTrigger className="w-full md:w-[200px]">
              <SelectValue placeholder="Filtrar por tipo" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="all">Todos los tipos</SelectItem>
              <SelectItem value="blanda">Competencias blandas</SelectItem>
              <SelectItem value="técnica">Competencias técnicas</SelectItem>
            </SelectContent>
          </Select>

          <Select
            value={yearFilter}
            onValueChange={(value) => setYearFilter(value)}
          >
            <SelectTrigger className="w-full md:w-[150px]">
              <SelectValue placeholder="Año" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="all">Todos los años</SelectItem>
              {YEARS_MOCK.map((year) => (
                <SelectItem key={year} value={year.toString()}>
                  {year}° Año
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
        </div>

        <div className="space-y-3">
          <div className="flex items-center gap-2">
            <h3 className="text-sm font-medium text-gray-700">
              Filtrar por tecnicatura:
            </h3>
            {tecnicaturaFilter !== "all" && (
              <Button
                variant="ghost"
                size="sm"
                onClick={() => setTecnicaturaFilter("all")}
                className="h-6 px-2 text-xs text-muted-foreground hover:text-foreground"
              >
                Limpiar filtro
              </Button>
            )}
          </div>
          <div className="flex flex-wrap gap-2">
            <Badge
              variant={tecnicaturaFilter === "all" ? "default" : "outline"}
              className={`cursor-pointer transition-all duration-200 ${
                tecnicaturaFilter === "all"
                  ? "bg-blue-600 text-white hover:bg-blue-700"
                  : "hover:bg-blue-50 hover:border-blue-300 hover:text-blue-700"
              }`}
              onClick={() => setTecnicaturaFilter("all")}
            >
              <GraduationCap className="mr-1 h-3 w-3" />
              Todas las tecnicaturas
            </Badge>
            {TECNICATURAS_MOCK.map((tecnicatura) => (
              <Badge
                key={tecnicatura.id}
                variant={
                  tecnicaturaFilter === tecnicatura.id ? "default" : "outline"
                }
                className={`cursor-pointer transition-all duration-200 ${
                  tecnicaturaFilter === tecnicatura.id
                    ? "bg-blue-600 text-white hover:bg-blue-700"
                    : "hover:bg-blue-50 hover:border-blue-300 hover:text-blue-700"
                }`}
                onClick={() => setTecnicaturaFilter(tecnicatura.id)}
              >
                <GraduationCap className="mr-1 h-3 w-3" />
                {tecnicatura.name.includes("Desarrollo")
                  ? "Des. Software"
                  : tecnicatura.name.includes("Gestión")
                  ? "Gest. Industrial"
                  : tecnicatura.name.includes("Mantenimiento")
                  ? "Mant. Industrial"
                  : tecnicatura.name.includes("Logística")
                  ? "Logística"
                  : tecnicatura.name}
              </Badge>
            ))}
          </div>
        </div>
      </div>

      {viewMode === "table" ? (
        <Card className="transition-all duration-200 hover:shadow-md">
          <CardHeader className="flex flex-row items-center justify-between">
            <div>
              <CardTitle>Competencias Registradas (Mock)</CardTitle>
              <CardDescription>
                Listado de competencias disponibles para evaluación (versión
                mock)
              </CardDescription>
            </div>
            <Button onClick={() => handleOpenForm()} className="ml-auto">
              <PlusCircle className="mr-2 h-4 w-4" />
              <span>Nueva Competencia</span>
            </Button>
          </CardHeader>
          <CardContent className="p-0">
            <Table>
              <TableHeader>
                <TableRow>
                  <TableHead className="w-[250px]">Nombre</TableHead>
                  <TableHead>Tipo</TableHead>
                  <TableHead>Años</TableHead>
                  <TableHead>Tecnicaturas</TableHead>
                  <TableHead className="text-right">Acciones</TableHead>
                </TableRow>
              </TableHeader>
              <TableBody>
                {filteredCompetencies.length > 0 ? (
                  filteredCompetencies.map((competency) => (
                    // Esto no se renderizará porque filteredCompetencies estará vacío
                    <TableRow
                      key={competency.id}
                      className="cursor-pointer hover:bg-muted/50 transition-colors"
                      onClick={() =>
                        console.log("Ver detalles (mock):", competency.id)
                      }
                    >
                      <TableCell className="font-medium">
                        <div>
                          <p className="font-medium">{competency.name}</p>
                          <p className="text-sm text-muted-foreground mt-1 line-clamp-2">
                            {competency.description}
                          </p>
                        </div>
                      </TableCell>
                      <TableCell>
                        <Badge
                          variant="outline"
                          className={
                            competency.type === "blanda"
                              ? "bg-blue-100 text-blue-700 border-blue-200"
                              : "bg-amber-100 text-amber-700 border-amber-200"
                          }
                        >
                          {competency.type === "blanda" ? (
                            <BookOpen className="mr-1 h-3 w-3" />
                          ) : (
                            <Briefcase className="mr-1 h-3 w-3" />
                          )}
                          {competency.type === "blanda" ? "Blanda" : "Técnica"}
                        </Badge>
                      </TableCell>
                      <TableCell>
                        <div className="flex flex-wrap gap-1">
                          {competency.years.map((year: number) => (
                            <Badge
                              key={year}
                              variant="outline"
                              className="text-xs"
                            >
                              {year}° Año
                            </Badge>
                          ))}
                        </div>
                      </TableCell>
                      <TableCell>
                        <div className="flex flex-wrap gap-1 max-w-[200px]">
                          {getTecnicaturaNames(competency.tecnicaturas).map(
                            (name, index) => (
                              <Badge
                                key={index}
                                variant="outline"
                                className="bg-blue-50/50 border-blue-200/70 text-blue-700 px-2.5 py-1 rounded-md transition-colors hover:bg-blue-100 hover:border-blue-300"
                              >
                                <GraduationCap className="mr-1 h-3 w-3" />
                                {name.includes("Desarrollo")
                                  ? "Des. Software"
                                  : name.includes("Gestión")
                                  ? "Gest. Industrial"
                                  : name.includes("Mantenimiento")
                                  ? "Mant. Industrial"
                                  : name}
                              </Badge>
                            )
                          )}
                        </div>
                      </TableCell>
                      <TableCell className="text-right">
                        <DropdownMenu>
                          <DropdownMenuTrigger asChild>
                            <Button
                              variant="ghost"
                              className="h-8 w-8 p-0"
                              onClick={(e) => e.stopPropagation()}
                            >
                              <span className="sr-only">Abrir menú</span>
                              <MoreHorizontal className="h-4 w-4" />
                            </Button>
                          </DropdownMenuTrigger>
                          <DropdownMenuContent align="end">
                            <DropdownMenuItem
                              onClick={(e) => {
                                e.stopPropagation();
                                console.log(
                                  "Ver detalles (mock):",
                                  competency.id
                                );
                              }}
                            >
                              <Eye className="mr-2 h-4 w-4" />
                              <span>Ver detalles</span>
                            </DropdownMenuItem>
                            <DropdownMenuItem
                              onClick={(e) => {
                                e.stopPropagation();
                                handleOpenForm(competency);
                              }}
                            >
                              <Edit className="mr-2 h-4 w-4" />
                              <span>Editar</span>
                            </DropdownMenuItem>
                            <DropdownMenuItem
                              onClick={(e) => {
                                e.stopPropagation();
                                console.log("Duplicando competencia (mock)");
                              }}
                            >
                              <Copy className="mr-2 h-4 w-4" />
                              <span>Duplicar</span>
                            </DropdownMenuItem>
                            <DropdownMenuSeparator />
                            <DropdownMenuItem
                              className="text-red-600"
                              onClick={(e) => {
                                e.stopPropagation();
                                handleDelete(competency.id);
                              }}
                            >
                              <Trash2 className="mr-2 h-4 w-4" />
                              <span>Eliminar</span>
                            </DropdownMenuItem>
                          </DropdownMenuContent>
                        </DropdownMenu>
                      </TableCell>
                    </TableRow>
                  ))
                ) : (
                  <TableRow>
                    <TableCell colSpan={5} className="text-center py-8">
                      <div className="flex flex-col items-center justify-center">
                        <Search className="h-10 w-10 text-muted-foreground mb-2" />
                        <h3 className="text-lg font-medium">
                          No se encontraron competencias
                        </h3>
                        <p className="text-sm text-muted-foreground mt-1">
                          (Esta es la vista mockeada, no hay datos para mostrar)
                        </p>
                      </div>
                    </TableCell>
                  </TableRow>
                )}
              </TableBody>
            </Table>
          </CardContent>
        </Card>
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
          <Card
            className="overflow-hidden transition-all duration-200 hover:shadow-lg border border-dashed border-slate-300 hover:border-primary/70 shadow-sm group cursor-pointer flex flex-col items-center justify-center p-6 h-full min-h-[220px]"
            onClick={() => handleOpenForm()}
          >
            <div className="text-center flex flex-col items-center gap-3">
              <div className="h-12 w-12 rounded-full bg-blue-100 dark:bg-blue-900/30 flex items-center justify-center text-primary">
                <PlusCircle className="h-6 w-6" />
              </div>
              <h3 className="text-xl font-bold">Nueva Competencia</h3>
              <p className="text-muted-foreground text-sm">
                Crea una nueva competencia para evaluar
              </p>
            </div>
          </Card>

          {filteredCompetencies.length > 0 ? (
            filteredCompetencies.map((competency) => (
              // Esto no se renderizará porque filteredCompetencies estará vacío
              <Card
                key={competency.id}
                className="overflow-hidden transition-all duration-200 hover:shadow-lg border border-slate-200 shadow-sm group cursor-pointer"
                onClick={() =>
                  console.log("Ver detalles (mock):", competency.id)
                }
              >
                <CardHeader className="pt-6 pb-2 px-5">
                  <div className="mb-2">
                    <Badge
                      variant="outline"
                      className={
                        competency.type === "blanda"
                          ? "bg-blue-100 text-blue-700 border-blue-200 font-medium"
                          : "bg-amber-100 text-amber-700 border-amber-200 font-medium"
                      }
                    >
                      {competency.type === "blanda" ? (
                        <BookOpen className="mr-1.5 h-3.5 w-3.5" />
                      ) : (
                        <Briefcase className="mr-1.5 h-3.5 w-3.5" />
                      )}
                      {competency.type === "blanda"
                        ? "Competencia blanda"
                        : "Competencia técnica"}
                    </Badge>
                  </div>
                  <CardTitle className="text-xl font-bold transition-colors group-hover:text-primary">
                    {competency.name}
                  </CardTitle>
                  <CardDescription className="line-clamp-2 mt-1.5 text-base">
                    {competency.description}
                  </CardDescription>
                </CardHeader>

                <CardContent className="px-5 py-3">
                  <div className="space-y-4">
                    <div>
                      <div className="flex items-center gap-2 mb-2">
                        <School className="h-4 w-4 text-muted-foreground" />
                        <h4 className="text-sm font-semibold text-muted-foreground">
                          Tecnicaturas
                        </h4>
                      </div>
                      <div className="flex flex-wrap gap-1.5">
                        {getTecnicaturaNames(competency.tecnicaturas).map(
                          (name, index) => (
                            <Badge
                              key={index}
                              variant="outline"
                              className="bg-blue-50/50 border-blue-200/70 text-blue-700 px-2.5 py-1 rounded-md transition-colors hover:bg-blue-100 hover:border-blue-300"
                            >
                              <GraduationCap className="mr-1 h-3 w-3" />
                              {name.includes("Desarrollo")
                                ? "Des. Software"
                                : name.includes("Gestión")
                                ? "Gest. Industrial"
                                : name.includes("Mantenimiento")
                                ? "Mant. Industrial"
                                : name}
                            </Badge>
                          )
                        )}
                      </div>
                    </div>

                    <div>
                      <div className="flex items-center gap-2 mb-2">
                        <Book className="h-4 w-4 text-muted-foreground" />
                        <h4 className="text-sm font-semibold text-muted-foreground">
                          Años académicos
                        </h4>
                      </div>
                      <div className="flex flex-wrap gap-1.5">
                        {competency.years.map((year: number) => (
                          <Badge
                            key={year}
                            variant="outline"
                            className="bg-slate-50/50 border-slate-200/70 text-slate-700 px-2.5 py-1 rounded-md"
                          >
                            {year}° Año
                          </Badge>
                        ))}
                      </div>
                    </div>
                  </div>
                </CardContent>

                <div className="absolute inset-0 bg-gradient-to-t from-background/80 via-transparent to-transparent opacity-0 group-hover:opacity-100 transition-opacity pointer-events-none" />
              </Card>
            ))
          ) : (
            <div className="col-span-1 md:col-span-2 flex flex-col items-center justify-center py-12">
              <Search className="h-12 w-12 text-muted-foreground mb-4" />
              <h3 className="text-lg font-medium">
                No se encontraron competencias
              </h3>
              <p className="text-muted-foreground mt-2">
                (Esta es la vista mockeada, no hay datos para mostrar)
              </p>
            </div>
          )}
        </div>
      )}

      <Dialog open={isDialogOpen} onOpenChange={setIsDialogOpen}>
        <DialogContent className="sm:max-w-[800px] max-h-[90vh] p-0">
          <DialogHeader className="px-8 pt-8 pb-6">
            <DialogTitle className="text-2xl font-semibold">
              {editingCompetency
                ? "Editar Competencia (Mock)"
                : "Nueva Competencia (Mock)"}
            </DialogTitle>
            <DialogDescription className="text-base mt-2">
              {editingCompetency
                ? "Modifica los datos de la competencia existente (versión mock)"
                : "Completa la información para crear una nueva competencia (versión mock)"}
            </DialogDescription>
          </DialogHeader>

          <Form {...form}>
            <form
              onSubmit={form.handleSubmit(onSubmit)}
              className="flex flex-col h-full"
            >
              <ScrollArea className="flex-1 px-8 max-h-[60vh]">
                <div className="space-y-8 pb-6">
                  <FormField
                    control={form.control}
                    name="name"
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel className="text-base font-medium">
                          Nombre de la competencia
                        </FormLabel>
                        <FormControl>
                          <Input
                            placeholder="Ej: Trabajo en equipo, Programación orientada a objetos..."
                            {...field}
                            className="h-12 text-base"
                          />
                        </FormControl>
                        <FormDescription className="text-sm">
                          Utiliza un nombre claro y descriptivo.
                        </FormDescription>
                        <FormMessage />
                      </FormItem>
                    )}
                  />

                  <FormField
                    control={form.control}
                    name="description"
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel className="text-base font-medium">
                          Descripción
                        </FormLabel>
                        <FormControl>
                          <Textarea
                            placeholder="Describe en qué consiste esta competencia..."
                            {...field}
                            className="min-h-[120px] text-base resize-none"
                          />
                        </FormControl>
                        <FormDescription className="text-sm">
                          Explica claramente qué implica esta competencia.
                        </FormDescription>
                        <FormMessage />
                      </FormItem>
                    )}
                  />

                  <FormField
                    control={form.control}
                    name="type"
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel className="text-base font-medium">
                          Tipo de competencia
                        </FormLabel>
                        <Select
                          onValueChange={field.onChange}
                          defaultValue={field.value}
                        >
                          <FormControl>
                            <SelectTrigger className="h-12 text-base">
                              <SelectValue placeholder="Selecciona el tipo de competencia" />
                            </SelectTrigger>
                          </FormControl>
                          <SelectContent>
                            <SelectItem value="blanda">
                              Competencia blanda
                            </SelectItem>
                            <SelectItem value="técnica">
                              Competencia técnica
                            </SelectItem>
                          </SelectContent>
                        </Select>
                        <FormDescription className="text-sm">
                          Las competencias blandas son habilidades
                          interpersonales, las técnicas son específicas de la
                          disciplina.
                        </FormDescription>
                        <FormMessage />
                      </FormItem>
                    )}
                  />

                  <FormField
                    control={form.control}
                    name="tecnicaturaYears"
                    render={({ field }) => (
                      <FormItem>
                        <FormLabel className="text-base font-medium">
                          Tecnicaturas y años aplicables
                        </FormLabel>
                        <div className="space-y-4 mt-3">
                          {TECNICATURAS_MOCK.map((tecnicatura) => {
                            const selectedYears =
                              field.value[tecnicatura.id] || [];

                            return (
                              <div
                                key={tecnicatura.id}
                                className="border rounded-lg p-4 space-y-3"
                              >
                                <div className="flex items-center space-x-3">
                                  <Checkbox
                                    id={`tecnicatura-${tecnicatura.id}`}
                                    checked={selectedYears.length > 0}
                                    onCheckedChange={(checked) => {
                                      const updatedTecnicaturaYears = {
                                        ...field.value,
                                        [tecnicatura.id]: checked
                                          ? [...YEARS_MOCK] // Usar YEARS_MOCK
                                          : [],
                                      };
                                      field.onChange(updatedTecnicaturaYears);
                                    }}
                                  />
                                  <label
                                    htmlFor={`tecnicatura-${tecnicatura.id}`}
                                    className="text-base font-semibold cursor-pointer flex-1"
                                  >
                                    {tecnicatura.name}
                                  </label>
                                </div>

                                {selectedYears.length > 0 && (
                                  <div className="ml-8 space-y-2">
                                    <p className="text-sm text-gray-600 mb-2">
                                      Selecciona los años específicos:
                                    </p>
                                    <div className="flex gap-4">
                                      {YEARS_MOCK.map((year) => (
                                        <div
                                          key={year}
                                          className="flex items-center space-x-2"
                                        >
                                          <Checkbox
                                            id={`${tecnicatura.id}-year-${year}`}
                                            checked={selectedYears.includes(
                                              year
                                            )}
                                            onCheckedChange={(checked) => {
                                              const updatedYears = checked
                                                ? [...selectedYears, year]
                                                : selectedYears.filter(
                                                    (y) => y !== year
                                                  );

                                              const updatedTecnicaturaYears = {
                                                ...field.value,
                                                [tecnicatura.id]: updatedYears,
                                              };
                                              field.onChange(
                                                updatedTecnicaturaYears
                                              );
                                            }}
                                          />
                                          <label
                                            htmlFor={`${tecnicatura.id}-year-${year}`}
                                            className="text-sm font-medium cursor-pointer"
                                          >
                                            {year}° Año
                                          </label>
                                        </div>
                                      ))}
                                    </div>
                                  </div>
                                )}
                              </div>
                            );
                          })}
                        </div>
                        <FormDescription className="text-sm">
                          Selecciona las tecnicaturas y años específicos donde
                          se aplicará esta competencia.
                        </FormDescription>
                        <FormMessage />
                      </FormItem>
                    )}
                  />
                </div>
              </ScrollArea>

              <DialogFooter className="px-8 py-6 border-t bg-gray-50/50">
                <div className="flex justify-between w-full">
                  <Button
                    type="button"
                    variant="outline"
                    onClick={() => setIsDialogOpen(false)}
                    className="px-6"
                  >
                    Cancelar
                  </Button>

                  <Button type="submit" className="px-8">
                    {editingCompetency ? (
                      <>
                        <Save className="mr-2 h-4 w-4" />
                        Guardar Cambios (Mock)
                      </>
                    ) : (
                      <>
                        <PlusCircle className="mr-2 h-4 w-4" />
                        Crear Competencia (Mock)
                      </>
                    )}
                  </Button>
                </div>
              </DialogFooter>
            </form>
          </Form>
        </DialogContent>
      </Dialog>
    </div>
  );
}
