import {useEffect, useState} from "react";
import "./App.css";
import {Button} from "./components/ui/button";
import {
    Card,
    CardHeader,
    CardTitle,
    CardDescription,
    CardContent,
    CardFooter,
} from "./components/ui/card";
import {ThemeToggle} from "./components/theme-toggle";
import {Loader} from "lucide-react";

function App() {
    const [greeting, setGreeting] = useState<string>("");
    const [rickMortData, setRickMortData] = useState<any>(null);
    const [loading, setLoading] = useState<boolean>(false)
    useEffect(() => {
        const fetch = async () => {
            await fetchRickMortData();
            await fetchHelloMessage();
        };
        fetch();
    }, []);

    async function fetchHelloMessage() {
        try {
            setLoading(true)
            const response = await fetch("api/Hello");
            const data = await response.text();
            setGreeting(data);
        } catch (error) {
            console.error("Failed to fetch hello message:", error);
            setGreeting("Failed to load greeting");
        } finally {
            setLoading(false)
        }
    }

    async function fetchRickMortData() {
        try {
            setLoading(true)
            const response = await fetch(
                `https://rickandmortyapi.com/api/character/${Math.floor(
                    Math.random() * 1000
                )}`
            );
            const data = await response.json();
            setRickMortData(data);
        } catch (error) {
            console.error("Failed to fetch Rick and Morty data:", error);
        } finally {
            setLoading(false)
        }
    }


    return (
        <div
            className="container mx-auto py-10 px-4 flex flex-col items-center justify-center min-h-screen bg-background text-foreground">
            <div className="absolute top-4 right-4">
                <ThemeToggle/>
            </div>

            <h1 className="text-3xl font-bold mb-6 text-center">
                Tiziano chupa pija y comeme bien los huevos de la tia
            </h1>

            <div className="grid grid-cols-1 gap-6 mb-10 items-center justify-center">
                <Card>
                    <CardHeader>
                        <CardTitle>Tiziano chupa pija</CardTitle>
                        <CardDescription>{greeting}</CardDescription>
                    </CardHeader>
                    <CardContent>
                        {rickMortData ? (
                            <div className="p-4 bg-muted rounded-md w-full h-full">
                                <img
                                    src={rickMortData.image}
                                    alt="Rick and Morty"
                                    className="w-full h-full object-cover"
                                />
                            </div>
                        ) : (
                            <Loader className="animate-spin"/>
                        )}
                    </CardContent>
                    <CardFooter>
                        <Button
                            variant={"outline"}
                            onClick={fetchRickMortData}
                            disabled={loading}
                        >
                            Cambiar Imagen
                        </Button>
                    </CardFooter>
                </Card>
            </div>
        </div>
    );
}

export default App;
