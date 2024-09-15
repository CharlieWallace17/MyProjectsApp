import { useQuery } from "@tanstack/react-query"

export const getBinaryConverterQuery = (binaryInput?: string) => useQuery({
    queryKey: [binaryInput],
    queryFn: async () => {
        try {
            const response = await fetch(`${import.meta.env.VITE_URL}/binaryConverter/${binaryInput}`, {
                headers: { "Content-Type": "application/json" },
                method: "GET",
            })

            return response.json()

        } catch (e) {
            console.log("ERROR", e)
        }
    },
    enabled: !!binaryInput,
}) 