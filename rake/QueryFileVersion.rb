require 'Win32api'

module QueryFileVersion
    def self.query_semantic_version(fileName)
        version = self.query(fileName)
        self.get_semantic_version(version)
    end

    def self.get_semantic_version(version)        
        throw ArgumentError.new 'Nil reference: "version" cannot be nil.' if version == nil

        version[/^\d+\.\d+\.\d+/]
    end

    def self.query(filename)        
        s = ''
        get_file_version_info_size = Win32API.new(
            'version.dll',
            'GetFileVersionInfoSize',
            ['P', 'P'],
            'L'
        )

        get_file_version_info = Win32API.new(
            'version.dll',
            'GetFileVersionInfo',
            ['P', 'L', 'L', 'P'],
            'L'
        )

        ver_query_value = Win32API.new(
            'Api-ms-win-core-version-l1-1-0.dll',
            'VerQueryValue',
            ['S', 'P', 'P', 'P'],
            'I'
        )

        vsize = get_file_version_info_size.call(filename, s)
        version = '0'

        if (vsize > 0)
          result = ' '*vsize
          get_file_version_info.call(filename, 0, vsize, result)

          rstring = result.unpack('v*').map{|s| s.chr if s<256}*''
          r = /FileVersion..(.*?)\000/.match(rstring)

          version = r[1] if r
        end

        version
    end
end
