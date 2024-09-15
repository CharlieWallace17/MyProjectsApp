import { createFileRoute } from '@tanstack/react-router'
import { useForm } from 'react-hook-form'
import { getBinaryConverterQuery } from '../api/queryApi'
import { useState } from 'react'

const Index = () => {
    return (
        <div className="p-2 h-screen w-screen">
            <div className="mx-auto text-center text-3xl font-bold mb-2">Binary Converter</div>
            <BinaryConverterForm />
        </div>
    )
}

export const Route = createFileRoute('/binaryConverter')({
    component: Index,
})

const BinaryConverterForm = () => {

    const { register, handleSubmit, watch, formState: { errors } } = useForm();
    const binaryInputValue = watch("binaryInput")

    const [input, setInput] = useState<string | undefined>()
    const { data } = getBinaryConverterQuery(input)

    const onSubmit = () => {
        setInput(binaryInputValue)
    }

    return (
        <form className="flex flex-col justify-around h-40 w-52 mx-auto" onSubmit={handleSubmit(onSubmit)}>
            <input
                className="border-solid border-black border-2 rounded p-1 mr-1 h-10 [appearance:textfield] [&::-webkit-outer-spin-button]:appearance-none [&::-webkit-inner-spin-button]:appearance-none"
                type="text"
                placeholder="Enter binary number..."
                {...register("binaryInput")}
            />
            <input value={data} disabled />
            {errors.exampleRequired && <span>This field is required</span>}
            <button className="bg-green-500 text-white hover:bg-green-700" type="submit">Submit</button>
        </form>
    )
}
