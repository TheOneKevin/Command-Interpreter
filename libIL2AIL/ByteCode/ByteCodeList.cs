using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libIL2AIL.ByteCode
{
    public enum OpCode : byte {
        reserved0 = 0x0,

        push,
        ipush,

        swap,
        call,

        reserved1,

        ret,
        iret,

        magic,
        error,

        cmp = 0x0A,
        je,
        jne,
        jb,
        jbe,
        ja,
        jae,
        methodDef,
        classDef,
        namespaceDef,

        //Insert raw minecraft command
        _raw = 0x2A,
        
        irv, //Create a new variable
        ro,  //Create an new object

        //Interger pop and object pop
        ipop,
        pop,

        //Push variable value into stack
        spush,
        lpush,
        bpush,

        //Pop value from stack into variable
        spop,
        lpop,
        bpop,

        //Load top stack value into register
        lods,
        lodl,
        lodb,
        lodi,

        //Move register value into stack
        movs,
        movl,
        movb,
        movi
    };
}
