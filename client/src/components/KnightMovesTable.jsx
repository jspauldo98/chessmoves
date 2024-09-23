export const KnightsMoveTable = ({ results, onDelete }) => {
    const handleDelete = (resultId) => {
        onDelete(resultId);
    };

    return (
        <div>
            <h2>Knight Moves Results</h2>
            <table>
                <thead>
                    <tr>
                        <th>Matrix</th>
                        <th>Unique Paths</th>
                        <th>Status</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    {results.map((result) => (
                        <tr key={result.id}>
                            <td>{result.matrix.name}</td>
                            <td>{result.uniquePathsCount == 0 ? "Calculating..." : result.uniquePathsCount}</td>
                            <td>{result.uniquePathsCount == 0 ? "IN_PROGRESS" : result.job.status}</td>
                            <td>
                                <button onClick={() => handleDelete(result.id)}>Delete</button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};
