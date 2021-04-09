SELECT
    *
FROM
    Data
WHERE
    1 = 1
/*% if (flag.HasValue) { */
    AND Flag = /*@ flag */0
/*% } */
ORDER BY
    Id
OFFSET /*@ offset */0 ROWS
FETCH NEXT /*@ limit */10 ROWS ONLY
