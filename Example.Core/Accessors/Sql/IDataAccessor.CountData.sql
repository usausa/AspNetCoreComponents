SELECT
    COUNT(*)
FROM
    Data
WHERE
    1 = 1
/*% if (flag.HasValue) { */
    AND Flag = /*@ flag */0
/*% } */
