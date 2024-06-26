import React, { ChangeEvent, SyntheticEvent } from 'react'

interface Props {
    search: string | undefined;
    onSearchSubmit: (e: SyntheticEvent) => void;
    handleSearchChange: (e: ChangeEvent<HTMLInputElement>) => void;
}

const Search: React.FC<Props> = (props: Props): JSX.Element => {
    return (
        <section className="relative bg-gray-100">
            <div className="max-w-4xl mx-auto p-6 space-y-6">
                <form
                    className="form relative flex flex-col w-full p-10 space-y-4 bg-darkBlue rounded-lg md:flex-row md:space-y-0 md:space-x-3"
                    onSubmit={props.onSearchSubmit}>
                    <input
                        className="flex-1 p-3 border-2 rounded placeholder-black focus:outline-none"
                        id="search-input"
                        placeholder="Search companies"
                        value={props.search}
                        onChange={props.handleSearchChange} />
                    <button className='p-3 rounded text-white border-2 hover:bg-white hover:text-black active:bg-gray-300' type='submit'>Search</button>
                </form>
            </div>
        </section>
    )
}

export default Search