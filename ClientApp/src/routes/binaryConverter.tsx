import { createFileRoute } from '@tanstack/react-router'

export const Route = createFileRoute('/binaryConverter')({
    component: Index,
})

function Index() {
    return (
        <div className="p-2">
            <h3>Binary Converter</h3>
        </div>
    )
}
