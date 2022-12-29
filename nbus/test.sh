

muxbuspoke16() {
        local x

        let x=$1
        x=$((x >> 11))
        tshwctl -a 0x70 -w $x
        let x=$1
        let x="(x & 0x7ff) | 0x8000"
        tshwctl -a 0x72 -w $x
        tshwctl -a 0x74 -w $2
}


muxbuspeek16() {
        local x

        let x=$1
        x=$((x >> 11))
        tshwctl -a 0x70 -w $x
        let x=$1
        let x="(x & 0x7ff) | 0x8000"
        tshwctl -a 0x72 -w $x
        tshwctl -a 0x74 -r
}


muxbuspoke8() {
        local x

        let x=$1
        x=$((x >> 11))
        tshwctl -a 0x70 -w $x
        let x=$1
        let x="(x & 0x7ff) | 0xc000"
        tshwctl -a 0x72 -w $x
        tshwctl -a 0x74 -w $2
}


muxbuspeek8() {
        local x

        let x=$1
        x=$((x >> 11))
        tshwctl -a 0x70 -w $x
        let x=$1
        let x="(x & 0x7ff) | 0xc000"
        tshwctl -a 0x72 -w $x
        tshwctl -a 0x74 -r
}