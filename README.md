# Command-Interpreter
Program Command Blocks like you do with a regular programming language!  
I have a habbit of commiting too often. IDK if its a good habbit (maybe if you lose everything).  
*Warning:* Contains some dirrrttyy code (performance is not a priority right now) :grin:
## Project files:
ProgramInterpreter <- Contains the GUI components of the program  
InterpreterEngine  <- The deprecated interpreter engine  
libIL2AIL          <- New and shiny engine. Uses the .NET Roslyn library to parse the code  
For now, only Windows users can enjoy this tool, and this is not going to change unless the Roslyn engine becomes cross platform, and someone comes and ports my code to other platforms.
### You can find the documentation at the project site!
(If you are still alive after reading them that is) :smirk:  
https://thecool1james.github.io/Command-Interpreter/  

I'm kinda addicted to the emojis now. :stuck_out_tongue_closed_eyes:

| MNEMONIC | Bytecode  | Stack In                               | Stack Out      | Description                                 |
|----------|-----------|----------------------------------------|----------------|---------------------------------------------|
| n/a      | 0x00      |                                        |                | Null string terminator                      |
| push     | 0x01      | object id                              | object id      | Pushes an object into stack                 |
| ipush    | 0x02      | variable id                            | variable value | Push an integer into stack                  |
| swap     | 0x03      | a, b                                   | b, a           | Swaps the top 2 values on the stack         |
| call     | 0x04      | arg n, arg n-1 ... arg 0, method ID    | return value   | Call library function                       |
| db       | 0x05      |                                        |                | RESERVED, see 0x2A                          |
| ret      | 0x06      |                                        |                | Return void to caller                       |
| iret     | 0x07      | return value                           | return value   | Return value after call, return to caller   |
| magic    | 0x08      |                                        |                | BREAKPOINT                                  |
| error    | 0x09      | error code                             | all cleared    | Activates command block to print exception. |
| n/a      | 0x0A-0x19 |                                        |                | RESERVED FOR LIBRARY AND REFERENCES         |
| _raw     | 0x2A      | arg n, arg n-1 ... arg 0               |                | Insert raw Minecraft comamnd*               |
| irv      | 0x2B      | variable type id, variable name hashed |                | Register a new empty variable               |
| ro       | 0x2C      | target object id, source object id     |                | Registers a new object                      |
| ipop     | 0x2D      | variable id                            |                | Pops integer from stack into variable       |
| pop      | 0x2E      | target object id, source object id     |                | Pops object from stack into another object  |
| spush    | 0x2F      | variable id                            | variable value | Push a string into string register          |
| lpush    | 0x30      | variable id                            | variable value | Push a long into stack                      |
| bpush    | 0x31      | variable id                            | variable value | Push a boolean into stack                   |
| spop     | 0x32      | variable id                            |                | Pops string from register** into variable   |
| lpop     | 0x33      | variable id                            |                | Pops long from stack into variable          |
| bpop     | 0x34      | variable id                            |                | Pops boolean from stack into variable       |
| lods     | 0x35      |                                        |                | Loads string into register                  |
| lodl     | 0x36      |                                        |                | Loads long into register                    |
| lodb     | 0x37      |                                        |                | Loads boolean into register                 |
| lodi     | 0x38      |                                        |                | Loads integer into register                 |
| pops     | 0x39      |                                        | value          | Loads register into stack                   |
| popl     | 0x3A      |                                        | value          | Loads register into stack                   |
| popb     | 0x3B      |                                        | value          | Loads register into stack                   |
| popi     | 0x3C      |                                        | value          | Loads register into stack                   |

*   - Note that the string stored in the stack must be null terminated with 0x0000 (yes, that's 4 bytes)  
    - Note if there is 0x05 following the the null termination, then it signals a reference to an argument. The 0x05 is then followed by the argument id  
    - The argument is structured like an object  

** String register differs from regular registers  

Stack and registers are abstract. Stack and registers can only hold up to 64 bits of data.

How it all operates:
Roslyn Parser ->[References]-> OPCODES ->[Library]-> Command Blocks

A [Reference] contains all the variable structures. It contains all the system-required method names hashed, ther corresponding IDs
and their structures. This will be written in CBIL, and compiled to the same bytecode above.
A [Library] contains all the code linked to the system-required methods. The names of each method should match that of the [Reference]. This will be
written in CBIL, and compiled to the bytecode above.
A [structure] is something to define the structure a variable/object that will be stored in the stack. Think of classes.

A variable will be stored in a variable dictionary with the id and it's corresponding type and hashed name.
An object will be stored in an object dictionary with the id and it's corresponding type and hashed name.

0x0A: A 2-byte variable structure ID follows. This will be the ID that will identify the variable type. (MNEMONIC: var)
0x0B: A SHA1 object structure ID follows. This will be the ID that will identify the object type. (MNEMONIC: obj)
0x0C: A 0x0000 goes in front of it to define the end of the structure (MNEMONIC: n/a)
0x0D: Defines the start of a method, it then is followed by a hashed string, the object id, and the flags (stack: array of N variable structures as it's arguments) (MNEMONIC: [name] [id] [flags]:)
0x0E: Defines the start of a class, it is then followed by a hashed string (the id of the object). (MNEMONIC: class [name]:)
0x0F: Namespace name (MNEMONIC: namespace [name]:

All methods + variables in a class would be names [namespace].[class].[name] to give each class an unique id name

Example CBIL script:
``` java
using CBIL;
namespace ExampleScript
{
    class Example
    {
        string exampleString = "Hello World";
        void main()
        {
            printLn(exampleString);
        }
    }
}
```

Bytecode Mnemonics:
```
namespace ExampleScript:
class ExampleScript.Example:
    irv 0x5 ExampleScript.Example.exampleString
    lods "Hello World"
    spop ExampleScript.Example.exampleString
    ExampleScript.Example.Main void private:
        spush ExampleScript.Example.exampleString
        call CBIL.Print.printLN

namespace CBIL:
class CBIL.Print:
    CBIL.Print.printLN void public static:
        irv 0x5 CBIL.Print.printLN.s
        spop CBIL.Print.printLN.s
        _raw "say @a" db CBIL.Print.printLN.s
        ret
```