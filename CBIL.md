| MNEMONIC | Bytecode | Stack IN                  | Stack OUT          | Usage             | Description                                                                             |
|----------|----------|---------------------------|--------------------|-------------------|-----------------------------------------------------------------------------------------|
| n/a      | 0x00     | base                      | base               | n/a               | RESERVED                                                                                |
| apush    | 0x01     | base                      | base, a            | push [aregister]  | Pushes an object ref into stack                                                         |
| bpush    | 0x02     | base                      | base, a            | push [register]   | Pushes a boolean into stack                                                             |
| ipush    | 0x03     | base                      | base, a            | push [register]   | Pushes an integer into stack                                                            |
| lpush    | 0x04     | base                      | base, a            | push [register]   | Pushes a long into stack                                                                |
| spush    | 0x05     | base                      | base, a            | push [register]   | Pushes a string into stack                                                              |
| apop     | 0x06     | base, a                   | base               | pop [register]    | Pops an object ref into a register                                                      |
| bpop     | 0x07     | base, a                   | base               | pop [register]    | Pops a boolean into a register                                                          |
| ipop     | 0x08     | base, a                   | base               | pop [register]    | Pops an integer into a register                                                         |
| lpop     | 0x09     | base, a                   | base               | pop [register]    | Pops a long into a register                                                             |
| spop     | 0x0A     | base, a                   | base               | pop [register]    | Pops a string into a register                                                           |
| mov      | 0x0B     | base                      | base               | mov [src], [dest] | Moves value into register/between registers                                             |
| ro       | 0x0C     | base, identifier, class   | base               | ro                | Registers a new object                                                                  |
| irv      | 0x0D     | base, identifier, type    | base               | irv               | Registers a new variable                                                                |
| _raw     | 0x2A     | base                      | base               | _raw "cmd"        | Inserts a Minecraft raw command. Only this allows the use of quotes                     |
| magic    | 0x0E     | base                      | base               | magic             | Breakpoint                                                                              |
| error    | 0x0F     | base                      | cleared            | error             | I don't know what this does                                                             |
| ret      | 0x10     | base                      | base               | ret               | Returns to caller. Only works with the call opcode.                                     |
| iret     | 0x11     | base, return value        | base, return value | iret              | Returns to caller with a return value on the top of the stack.                          |
| call     | 0x12     | base, arg0, arg1 ... argn | base, ???          | call [function]   | Calls a function, arguments should be pushed.                                           |
| swap     | 0x13     | base, a, b                | base, b, a         | swap              | Swaps the top 2 stack elements.                                                         |
| cmp      | 0x14     | base                      | base               | cmp a, b          | Compares two registers                                                                  |
| jmp      | 0x15     | base                      | base, ???          | jmp [symbol]      | Jumps to a point in code. Different from call because control doesn't return to caller. |
| jne      | 0x16     | base                      | base, ???          | jne [symbol]      | Jumps only if a != b, else continue execution.                                          |
| jbe      | 0x17     | base                      | base, ???          | jbe [symbol]      | Jumps only if a <= b, else continue execution.                                          |
| jae      | 0x18     | base                      | base, ???          | jae [symbol]      | Jumps only if a >= b, else continue execution.                                          |
| je       | 0x19     | base                      | base, ???          | je [symbol]       | Jumps only if a = b, else continue execution.                                           |
| jb       | 0x1A     | base                      | base, ???          | jb [symbol]       | Jumps only if a < b, else continue execution.                                           |
| ja       | 0x1B     | base                      | base, ???          | ja [symbol]       | Jumps only if a > b, else continue execution.                                           |