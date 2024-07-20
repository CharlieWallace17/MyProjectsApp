import { useQuery } from "@tanstack/react-query"

export const getBinaryConverterQuery = (binaryInput?: string) => useQuery({
    queryKey: [binaryInput],
    queryFn: async () => {
        const response = await fetch(`${import.meta.env.VITE_URL}/binaryConverter?binaryInput=${binaryInput}`, {
            mode: 'no-cors'
        })
        if (!response.ok)
            throw new Error()
        return response.json()
    },
    enabled: !!binaryInput?.length
}) 