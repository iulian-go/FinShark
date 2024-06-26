import React from 'react'

interface Props {
    config: any;
    data: any;
}

const Table = (props: Props) => {
    const renderedHeader = props.config.map((config :any) => {
        return (
            <th key={config.label} className='p-4 text-left text-xs font-medium text-gray-500 uppercase tracking-wider'>
                {config.label}
            </th>
        )
    });

    const renderedRows = props.data.map((company: any, index: number) => {
        return (
            <tr key={index}>
                {props.config.map((val: any, tdindex: number) => {
                    return (
                        <td key={tdindex} className='p-4 whitespace-nowrap text-left text-sm font-normal text-gray-900'>
                            {val.render(company)}
                        </td>
                    );
                })}
            </tr>
        )
    });

    return (
        <div className='bg-white shadow rounded-lg p-4 sm:p-6 xl:p-8'>
            <table className='min-w-full divide-y divide-gray-200 m-5'>
                <thead className='bg-gray-100'>
                    <tr>
                        {renderedHeader}
                    </tr>
                </thead>
                <tbody>
                    {renderedRows}
                </tbody>
            </table>
        </div>
    )
}

export default Table