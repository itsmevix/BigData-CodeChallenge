import java.io.*;
import java.util.*;
import org.apache.hadoop.fs.Path;
import org.apache.hadoop.conf.*;
import org.apache.hadoop.io.*;
import org.apache.hadoop.mapred.*;
import org.apache.hadoop.util.*;
import org.apache.hadoop.mapred.lib.*;


public class SortLogs{
        static class HttpLogsMap extends MapReduceBase implements Mapper<LongWritable, Text, Text, Text> {
                public void map(LongWritable key, Text value,OutputCollector<Text, Text> output, Reporter reporter) throws IOException {
                	
                	String line = value.toString();
    		        int userIndex = line.length() - 1; // Pega última posição do userId, excluindo as aspas
    		        String userId = line.substring(userIndex - 36, userIndex); // Extrai o userId da string excluindo as aspas
    		        
                    output.collect(new Text(userId),new Text(line)); // key = userID, value = a linha completa
                }
        }

        static class HttpLogsReduce extends MapReduceBase implements Reducer<Text, Text, Text, Text> {
                public void reduce(Text key, Iterator<Text> values,OutputCollector<Text, Text> output, Reporter reporter) throws IOException {
                	
                        while (values.hasNext()) {
                                output.collect(key, values.next());
                        }
                }
        }


        static class FileOutput extends MultipleTextOutputFormat<Text, Text> {
        		
        		// generateFileNameForKeyValue
        		// Método que gera o nome do arquivo de acordo com a key (userId)
                protected String generateFileNameForKeyValue(Text key, Text value,String name) {
                	
                        return key.toString();
                }
        }


        public static void main(String[] args) throws Exception {

                Configuration config=new Configuration();
                JobConf jconfig = new JobConf(config,SortLogs.class);

                jconfig.setOutputKeyClass(Text.class);
                jconfig.setMapOutputKeyClass(Text.class);
                jconfig.setOutputValueClass(Text.class);

                jconfig.setMapperClass(HttpLogsMap.class);
                jconfig.setReducerClass(HttpLogsReduce.class);

                jconfig.setInputFormat(TextInputFormat.class);
                jconfig.setOutputFormat(FileOutput.class);

                FileInputFormat.setInputPaths(jconfig,args[0]);
                FileOutputFormat.setOutputPath(jconfig,new Path(args[1]));
                JobClient.runJob(jconfig);

        }
}