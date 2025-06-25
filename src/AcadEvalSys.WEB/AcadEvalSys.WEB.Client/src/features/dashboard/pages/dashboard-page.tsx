export default function DashboardPage() {
  return (
    <div className="space-y-6">
      <div>
        <h1 className="text-3xl font-bold">Panel Principal</h1>
        <p className="text-muted-foreground">Bienvenido al sistema AcadEval</p>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        <div className="bg-card p-6 rounded-lg border">
          <h3 className="font-semibold mb-2">Encuestas</h3>
          <p className="text-sm text-muted-foreground">
            Gestionar encuestas académicas
          </p>
        </div>
        <div className="bg-card p-6 rounded-lg border">
          <h3 className="font-semibold mb-2">Evaluaciones</h3>
          <p className="text-sm text-muted-foreground">
            Evaluaciones por competencias
          </p>
        </div>
        <div className="bg-card p-6 rounded-lg border">
          <h3 className="font-semibold mb-2">Administración</h3>
          <p className="text-sm text-muted-foreground">
            Gestión de personal y tecnicaturas
          </p>
        </div>
      </div>
    </div>
  );
}
