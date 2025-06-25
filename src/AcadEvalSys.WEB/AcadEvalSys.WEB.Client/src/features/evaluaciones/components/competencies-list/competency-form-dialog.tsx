import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { PlusCircle, Save } from "lucide-react";
import {
  Dialog,
  DialogContent,
  DialogDescription,
  DialogFooter,
  DialogHeader,
  DialogTitle,
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
import { Input } from "@/shared/components/ui/input";
import { Textarea } from "@/shared/components/ui/textarea";
import { Button } from "@/shared/components/ui/button";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/shared/components/ui/select";
import { ScrollArea } from "@/shared/components/ui/scroll-area";
import { Competency } from "../../types";
import { competencyFormSchema } from "../../schemas/competency";

interface CompetencyFormDialogProps {
  open: boolean;
  onOpenChange: (open: boolean) => void;
  competency?: Competency | null;
  onSubmit: (data: Competency) => void;
}

export function CompetencyFormDialog({
  open,
  onOpenChange,
  competency,
  onSubmit,
}: CompetencyFormDialogProps) {
  const isEditing = !!competency;

  const form = useForm<z.infer<typeof competencyFormSchema>>({
    resolver: zodResolver(competencyFormSchema),
    defaultValues: {
      name: competency?.name || "",
      description: competency?.description || "",
      type: competency?.type,
    },
  });

  const handleSubmit = (values: z.infer<typeof competencyFormSchema>) => {
    onSubmit({
      id: competency?.id || "",
      name: values.name,
      description: values.description,
      type: values.type,
    });
    onOpenChange(false);
    form.reset();
  };

  const handleClose = () => {
    onOpenChange(false);
    form.reset();
  };

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent className="sm:max-w-[800px] max-h-[90vh] p-0">
        <DialogHeader className="px-8 pt-8 pb-6">
          <DialogTitle className="text-2xl font-semibold">
            {isEditing ? "Editar Competencia" : "Nueva Competencia"}
          </DialogTitle>
          <DialogDescription className="text-base mt-2">
            {isEditing
              ? "Modifica los datos de la competencia existente"
              : "Completa la información para crear una nueva competencia"}
          </DialogDescription>
        </DialogHeader>

        <Form {...form}>
          <form
            onSubmit={form.handleSubmit(handleSubmit)}
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
              </div>
            </ScrollArea>

            <DialogFooter className="px-8 py-6 border-t bg-gray-50/50">
              <div className="flex justify-between w-full">
                <Button
                  type="button"
                  variant="outline"
                  onClick={handleClose}
                  className="px-6"
                >
                  Cancelar
                </Button>

                <Button type="submit" className="px-8">
                  {isEditing ? (
                    <>
                      <Save className="mr-2 h-4 w-4" />
                      Guardar Cambios
                    </>
                  ) : (
                    <>
                      <PlusCircle className="mr-2 h-4 w-4" />
                      Crear Competencia
                    </>
                  )}
                </Button>
              </div>
            </DialogFooter>
          </form>
        </Form>
      </DialogContent>
    </Dialog>
  );
}
